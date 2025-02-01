import { Component, inject, signal } from '@angular/core';
import { BrnDialogRef, injectBrnDialogContext } from '@spartan-ng/ui-dialog-brain';
import { Shipment } from '../../_model/shipment';
import { HlmDialogHeaderComponent } from "../../../../libs/ui/ui-dialog-helm/src/lib/hlm-dialog-header.component";
import { HlmDialogFooterComponent } from "../../../../libs/ui/ui-dialog-helm/src/lib/hlm-dialog-footer.component";
import { ReceiveShipment } from '../../_model/shipmentReceive';
import { toast } from 'ngx-sonner';
import { ShipmentReceiveService } from '../../_services/shipment-receive.service';
import { NgFor } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shipment-receive-receipt',
  standalone: true,
  imports: [HlmDialogHeaderComponent, HlmDialogFooterComponent, NgFor],
  templateUrl: './shipment-receive-receipt.component.html',
  styleUrl: './shipment-receive-receipt.component.scss'
})
export class ShipmentReceiveReceiptComponent {

  private readonly _dialogRef = inject<BrnDialogRef<ReceiveShipment>>(BrnDialogRef);
  private readonly shipmentReceiveService = inject(ShipmentReceiveService);
  private readonly _dialogContext = injectBrnDialogContext<{ shipment: ReceiveShipment }>();
  private router = inject(Router);
  shipment = this._dialogContext.shipment

  Submit() {
    if (this.shipment.shipmentReceives?.length <= 0 ) {
      toast.error("Unsuccessful", {
        description: "Please select item receive"

      })
      return
    }
    console.log("Submit")

   // if (this.availableShipment()!.shipmentItems.length > 0)
    const missing: boolean = this.shipment.shipmentReceives?.some((receive) => receive.MissingQuantity > 0)

    if(missing) this.shipment = {...this.shipment, status: 'Partial'}
    this.shipmentReceiveService.receiveItemShip(this.shipment).subscribe({
      next: () => {
        this._dialogRef.close();
        toast.success("Successful", {
          description: "Receive items successfully"
        })
        this.router.navigate(['/shipment'], { queryParams: {tab: 'inbound'} })
      }

    })


  }
}
