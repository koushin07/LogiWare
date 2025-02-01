import { Component, input } from '@angular/core';
import { Ownership } from '../../_model/ownership';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HlmInputDirective } from '@spartan-ng/ui-input-helm';
import { HlmLabelDirective } from '@spartan-ng/ui-label-helm';

import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective,
  HlmCardFooterDirective,
  HlmCardHeaderDirective,
  HlmCardTitleDirective,
} from '@spartan-ng/ui-card-helm';
@Component({
  selector: 'app-item-detail-owner',
  standalone: true,
  imports: [
    HlmInputDirective,
    HlmLabelDirective,
    NgIf,
    FormsModule,
    HlmCardContentDirective,
    HlmCardDescriptionDirective,
    HlmCardDirective,
    HlmCardFooterDirective,
    HlmCardHeaderDirective,
    HlmCardTitleDirective,],
  templateUrl: './item-detail-owner.component.html',
  styleUrl: './item-detail-owner.component.scss'
})
export class ItemDetailOwnerComponent {
  owner = input.required<Ownership>()
}
