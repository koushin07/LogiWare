import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Params, Router } from "@angular/router";
import { Shipment } from "../../_model/shipment";
import { ShipmentService } from "../../_services/shipment.service";
import { ShipmentItem } from "../../_model/shipmentItem";
import { FormsModule } from "@angular/forms";
import { NgForOf, NgIf } from "@angular/common";
import { Personnel } from "../../_model/personnel";
import { Site } from "../../_model/site";
import { HlmButtonDirective } from "@spartan-ng/ui-button-helm";
import { HlmInputDirective } from "@spartan-ng/ui-input-helm";
import {
  HlmAlertDialogActionButtonDirective,
  HlmAlertDialogCancelButtonDirective,
  HlmAlertDialogComponent,
  HlmAlertDialogContentComponent, HlmAlertDialogDescriptionDirective, HlmAlertDialogFooterComponent,
  HlmAlertDialogHeaderComponent, HlmAlertDialogTitleDirective
} from "@spartan-ng/ui-alertdialog-helm";
import { BrnAlertDialogContentDirective, BrnAlertDialogTriggerDirective } from "@spartan-ng/ui-alertdialog-brain";
import { HlmSeparatorDirective } from "@spartan-ng/ui-separator-helm";
import { toast } from "ngx-sonner";
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective, HlmCardFooterDirective,
  HlmCardHeaderDirective,
  HlmCardTitleDirective
} from "@spartan-ng/ui-card-helm";
import { HlmIconComponent } from "@spartan-ng/ui-icon-helm";
import { provideIcons } from "@ng-icons/core";
import { lucideX, lucideArrowUpCircle, lucideArrowDownCircle } from '@ng-icons/lucide';
import { ReceiveShipment } from "../../_model/shipmentReceive";
import { ShipmentReceiveService } from "../../_services/shipment-receive.service";
import {
  HlmDialogComponent,
  HlmDialogContentComponent,
  HlmDialogFooterComponent,
  HlmDialogHeaderComponent,
  HlmDialogService
} from "@spartan-ng/ui-dialog-helm";
import {
  BrnDialogContentDirective, BrnDialogDescriptionDirective,
  BrnDialogTitleDirective,
  BrnDialogTriggerDirective
} from "@spartan-ng/ui-dialog-brain";
import { HlmScrollAreaComponent } from "@spartan-ng/ui-scrollarea-helm";
import { HlmCaptionComponent, HlmTableComponent, HlmThComponent, HlmTrowComponent } from "@spartan-ng/ui-table-helm";
import { HlmTdComponent } from "../../../../libs/ui/ui-table-helm/src/lib/hlm-td.component";
import { ShipmentReceiveReceiptComponent } from '../../component/shipment-receive-receipt/shipment-receive-receipt.component';
import { AuthenticationService } from '../../_services/authentication.service';
// @ts-ignore
@Component({
  selector: 'app-shipment-receive',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    NgIf,
    BrnAlertDialogTriggerDirective,
    BrnAlertDialogContentDirective,
    HlmSeparatorDirective,
    HlmAlertDialogComponent,
    HlmButtonDirective,
    HlmAlertDialogContentComponent,
    HlmAlertDialogHeaderComponent,
    HlmAlertDialogTitleDirective,
    HlmAlertDialogDescriptionDirective,
    HlmAlertDialogFooterComponent,
    HlmAlertDialogCancelButtonDirective,
    HlmAlertDialogActionButtonDirective,
    HlmInputDirective,
    HlmCardDirective,
    HlmCardHeaderDirective,
    HlmCardTitleDirective,
    HlmCardDescriptionDirective,
    HlmCardContentDirective,
    HlmCardFooterDirective,
    HlmIconComponent,
    HlmDialogComponent,
    BrnDialogTriggerDirective,
    BrnDialogContentDirective,
    HlmDialogContentComponent,
    HlmDialogHeaderComponent,
    BrnDialogTitleDirective,
    BrnDialogDescriptionDirective,
    HlmDialogFooterComponent,
    HlmScrollAreaComponent,
    HlmTableComponent,
    HlmCaptionComponent,
    HlmTrowComponent,
    HlmThComponent,
    HlmTdComponent
  ],

  providers: [provideIcons({ lucideX, lucideArrowUpCircle, lucideArrowDownCircle })],
  templateUrl: './shipment-receive.component.html',
  styleUrl: './shipment-receive.component.scss'
})
export class ShipmentReceiveComponent implements OnInit {


  availableShipment = signal<Shipment | null>(null)
  selectedShipment = signal<Shipment | null>(null)
  receiveShipment = signal<ReceiveShipment | null>(null)

  private _quantityTracker = [{quantity: 0, code: ''}]

  private _route = inject(ActivatedRoute)
  private _shipmentService = inject(ShipmentService)
  private _router = inject(Router)
  private _shipmentReceiveService = inject(ShipmentReceiveService)
  private _hlmDialogService = inject(HlmDialogService)
  private _authService = inject(AuthenticationService)


  ngOnInit(): void {

    this._route.params.subscribe((params: Params) => {
        this.loadAvailableShipment(params['code'])
    })
  }

  private loadAvailableShipment(code: string): void {
    this._shipmentService.getShipmentByCode(code)
      .subscribe((shipment: Shipment) =>this.handleShipmentResponse(shipment));
  }

  private handleShipmentResponse(res: Shipment): void {
    console.log(res)
    const updatedShipment = {
      ...res,
      shipmentItems: res.shipmentItems.map(si => ({
        ...si,
        quantity: si.shipmentReceives
          ? si.quantity! - si.shipmentReceives.reduce((total, sr) => total + sr.quantityReceived, 0)
          : si.quantity,

      }))
    };

    this.availableShipment.set(updatedShipment);
    this.selectedShipment.set({ ...res, shipmentItems: [] });

    this.receiveShipment.set({
      ...this.receiveShipment()!,
      shipmentCode: res.shipmentCode,
      driver: res.driver,
      authorizedBy: res.authorizedBy,
      shipmentReceives: [],
      siteId: this._authService.site().id,
    });
  }

  openDialog(): void {
    console.log(this.receiveShipment)
    const dialogRef = this._hlmDialogService.open(ShipmentReceiveReceiptComponent, {
      context: { shipment: this.receiveShipment() }
    });
    dialogRef.closed$.subscribe(shipment => {
      if (shipment) {
        console.log('Selected shipment:', shipment);
      }
    });
  }

  receiveItem(shipmentItem: ShipmentItem): void {
    if (shipmentItem.receive === 0 || shipmentItem.receive === null) return

    if (this.isAlreadyReceived(shipmentItem)) {
      toast.error('Item already received.');
      return
    }
     const remainingQuantity = this.getRemainingQuantity(shipmentItem)

    if (remainingQuantity < 0) {
      toast.error('Cannot receive more than the available quantity.');
      return
    }

    this.updateReceiveShipment(shipmentItem, remainingQuantity)
    this.updateTrackShipmentQuantity(shipmentItem)
    this.addItemToSelectedShipment(shipmentItem)
    this.updateAvailableShipment(shipmentItem);

  }
  private updateReceiveShipment(shipmentItem: ShipmentItem, quantity: number): void {
    const receive = this.receiveShipment()
    console.log(shipmentItem.receive)
    if (receive?.shipmentReceives) {
      receive.shipmentReceives.push({
        MissingQuantity: quantity,
        ReceiveQuantity: shipmentItem.receive!,
        shipmentItemCode: shipmentItem.shipmentItemCode,
        item: shipmentItem.ownership.item
      })
      this.receiveShipment.set(receive)

    }

  }


  private updateAvailableShipment(shipmentItem: ShipmentItem) {
    const availableShipmentData = this.availableShipment()
    if (!availableShipmentData) return
    const index = availableShipmentData.shipmentItems.findIndex(
      item => item.shipmentItemCode === shipmentItem.shipmentItemCode
    );

    if (index !== -1) {
      const itemToUpdate = availableShipmentData.shipmentItems[index];
      if (itemToUpdate.quantity === itemToUpdate.receive!) {
        availableShipmentData.shipmentItems.splice(index, 1);
      } else {
        itemToUpdate.quantity = this.getRemainingQuantity(itemToUpdate);

        console.log("hit")
      }
      itemToUpdate.receive = null;
    }
    this.availableShipment.set(availableShipmentData);

  }
  private addItemToSelectedShipment(shipmentItem: ShipmentItem) {
    const updatedSelectedShipment = this.selectedShipment()
    if (!updatedSelectedShipment) return
    updatedSelectedShipment.shipmentItems.push(
      {
        ...shipmentItem,
        quantity: shipmentItem.receive!
      }
    );

    this.selectedShipment.set(updatedSelectedShipment);
  }
  updateTrackShipmentQuantity(shipmentItem: ShipmentItem) {
    this._quantityTracker.push({ quantity: shipmentItem.quantity!, code: shipmentItem.shipmentItemCode })

  }
  private getRemainingQuantity(shipmentItem: ShipmentItem) {
    return shipmentItem.quantity! - shipmentItem.receive!
  }
  private isAlreadyReceived(shipmentItem: ShipmentItem): boolean {
    return !!this.selectedShipment()!.shipmentItems.find(si => si.id === shipmentItem.id);
  }

  removeItem(shipmentItem: ShipmentItem): void{
    this.returnItemToAvailableShipment(shipmentItem);
    this.removeItemFromSelectedShipment(shipmentItem);
    this.removeFromReceiveShipment(shipmentItem);
  }
  private returnItemToAvailableShipment(shipmentItem: ShipmentItem) {
    const availableShipmentData = this.availableShipment()
    if (!availableShipmentData) return
    shipmentItem.receive = null
    const originalQuantity = this.getTrackQuantity(shipmentItem.shipmentItemCode)

    const index = availableShipmentData.shipmentItems.findIndex(i => i.id === shipmentItem.id);

    if (index !== -1) {
      availableShipmentData.shipmentItems[index].quantity = originalQuantity;

    } else {
      availableShipmentData.shipmentItems.push({ ...shipmentItem, quantity: originalQuantity });
    }


    this.availableShipment.set(availableShipmentData);

  }
  private removeItemFromSelectedShipment(shipmentItem: ShipmentItem) {
    const updatedSelectedShipment = this.selectedShipment();
    updatedSelectedShipment!.shipmentItems = updatedSelectedShipment!.shipmentItems.filter(
      si => si.id !== shipmentItem.id
    );
    this.selectedShipment.set(updatedSelectedShipment);

  }
  private removeFromReceiveShipment(shipmentItem: ShipmentItem) {
    this.receiveShipment.set({
      ...this.receiveShipment()!,
      shipmentReceives: this.receiveShipment()!.shipmentReceives?.filter(
        sr => sr.shipmentItemCode !== shipmentItem.shipmentItemCode
      )
    });

  }

  private getTrackQuantity(code: string) {
    return this._quantityTracker.find(qt => qt.code === code)?.quantity || 0;
  }









}

