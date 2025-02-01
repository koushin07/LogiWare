import {Component, input, output} from '@angular/core';
import {Shipment} from "../../_model/shipment";
import {NgForOf, NgIf} from "@angular/common";
import {HlmScrollAreaComponent} from "@spartan-ng/ui-scrollarea-helm";
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective, HlmCardFooterDirective,
  HlmCardHeaderDirective,
  HlmCardTitleDirective
} from "@spartan-ng/ui-card-helm";
import {HlmButtonDirective} from "@spartan-ng/ui-button-helm";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-shipment-card',
  standalone: true,
  imports: [
    NgIf,
    HlmScrollAreaComponent,
    HlmCardDirective,
    HlmCardHeaderDirective,
    HlmCardTitleDirective,
    HlmCardDescriptionDirective,
    HlmCardContentDirective,
    HlmScrollAreaComponent,
    HlmCardFooterDirective,
    HlmCardFooterDirective,
    NgForOf,
    HlmButtonDirective,
    RouterLink
  ],
  templateUrl: './shipment-card.component.html',
  styleUrl: './shipment-card.component.scss'
})
export class ShipmentCardComponent {
 shipment = input.required<Shipment | null>()
}
