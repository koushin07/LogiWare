import { DatePipe, NgClass, NgFor } from '@angular/common';
import { Component, inject, input } from '@angular/core';
import { ItemHistory } from '../../_model/itemHistory';

import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective,
  HlmCardFooterDirective,
  HlmCardHeaderDirective,
  HlmCardTitleDirective,
} from '@spartan-ng/ui-card-helm';
import { ShipmentService } from '../../_services/shipment.service';

@Component({
  selector: 'app-item-detail-history',
  standalone: true,
  imports: [
    DatePipe,
    NgFor,
    NgClass,
    HlmCardContentDirective,
    HlmCardDescriptionDirective,
    HlmCardDirective,
    HlmCardFooterDirective,
    HlmCardHeaderDirective,
    HlmCardTitleDirective,],
  templateUrl: './item-detail-history.component.html',
  styleUrl: './item-detail-history.component.scss'
})
export class ItemDetailHistoryComponent {
  histories = input.required<ItemHistory[]>();
  private shipmentService = inject(ShipmentService)

  getStatusClass(status: ItemHistory['status']): string {
   return this.shipmentService.getStatusClass(status)
  }
}
