import {Component, input, signal} from '@angular/core';
import {Shipment} from "../../../_model/shipment";
import {HlmScrollAreaComponent} from "@spartan-ng/ui-scrollarea-helm";
import {HlmIconComponent} from "@spartan-ng/ui-icon-helm";
import {provideIcons} from "@ng-icons/core";
import {lucideChevronRight, lucidePlus} from "@ng-icons/lucide";
import {ShipmentCardComponent} from "../../shipment-card/shipment-card.component";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-to-receive-tab',
  standalone: true,
  imports: [
    HlmScrollAreaComponent,
    HlmIconComponent,
    ShipmentCardComponent,
    NgForOf,
    NgIf
  ],
  providers: [provideIcons({lucideChevronRight, lucidePlus })],
  templateUrl: './to-receive-tab.component.html',
  styleUrl: './to-receive-tab.component.scss'
})
export class ToReceiveTabComponent {
  selectShipment = signal<Shipment | null>(null)
  shipments = input.required<Shipment[]>()

  getShipment(shipment: Shipment){
    this.selectShipment.set(shipment)
  }
}
