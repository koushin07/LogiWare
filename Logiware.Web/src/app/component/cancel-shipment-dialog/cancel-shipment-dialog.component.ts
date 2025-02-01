import { Component, inject } from '@angular/core';
import { BrnDialogRef, injectBrnDialogContext } from '@spartan-ng/ui-dialog-brain';
import { Shipment } from '../../_model/shipment';
import { BrnDialogContentDirective, BrnDialogTriggerDirective } from '@spartan-ng/ui-dialog-brain';
import {
  HlmDialogComponent,
  HlmDialogContentComponent,
  HlmDialogDescriptionDirective,
  HlmDialogFooterComponent,
  HlmDialogHeaderComponent,
  HlmDialogTitleDirective,
} from '@spartan-ng/ui-dialog-helm';
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { ShipmentService } from '../../_services/shipment.service';

@Component({
  selector: 'app-cancel-shipment-dialog',
  standalone: true,
  imports: [
    HlmDialogComponent,
    HlmDialogContentComponent,
    HlmDialogDescriptionDirective,
    HlmDialogFooterComponent,
    HlmDialogHeaderComponent,
    HlmDialogTitleDirective,
    HlmButtonDirective,
  ],
  templateUrl: './cancel-shipment-dialog.component.html',
  styleUrl: './cancel-shipment-dialog.component.scss'
})
export class CancelShipmentDialogComponent {

  private readonly _dialogRef = inject<BrnDialogRef<Shipment>>(BrnDialogRef);
  private readonly _dialogContext = injectBrnDialogContext<{ shipment: Shipment }>();
  private readonly _shipmentService = inject<ShipmentService>(ShipmentService);
  protected readonly shipment = this._dialogContext.shipment;

  cancelShipment() {
    this._shipmentService.cancelShipment(this.shipment.shipmentCode ).subscribe();
  }

}
