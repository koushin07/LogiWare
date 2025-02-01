import { Component, HostListener, OnInit, signal, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap } from "@angular/router";
import { AuthenticationService } from "../../_services/authentication.service";
import { take } from "rxjs";
import { ItemService } from "../../_services/item.service";
import { Ownership } from "../../_model/ownership";
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective, HlmCardFooterDirective, HlmCardHeaderDirective,
  HlmCardTitleDirective
} from '@spartan-ng/ui-card-helm';
import { FormsModule, NgForm } from "@angular/forms";
import { Item } from "../../_model/item";
import { ItemHistory } from "../../_model/itemHistory";
import { HlmInputDirective } from "@spartan-ng/ui-input-helm";
import { HlmLabelDirective } from "@spartan-ng/ui-label-helm";
import { Site } from "../../_model/site";
import { HlmScrollAreaComponent } from "@spartan-ng/ui-scrollarea-helm";
import { HlmSeparatorDirective } from "@spartan-ng/ui-separator-helm";
import { DatePipe, NgForOf, NgIf } from "@angular/common";
import { HlmButtonDirective } from "@spartan-ng/ui-button-helm";
import {
  HlmAlertDescriptionDirective,
  HlmAlertDirective,
  HlmAlertIconDirective,
  HlmAlertTitleDirective
} from "@spartan-ng/ui-alert-helm";
import { HlmIconComponent } from "@spartan-ng/ui-icon-helm";
import { provideIcons } from "@ng-icons/core";
import { lucideAlertTriangle } from "@ng-icons/lucide";
import { Toast } from "ngx-toastr";
import { toast } from "ngx-sonner";
import { BrnSelectImports } from '@spartan-ng/ui-select-brain';
import { HlmSelectImports } from '@spartan-ng/ui-select-helm';
import { ItemDetailHistoryComponent } from "../../component/item-detail-history/item-detail-history.component";
import { ItemDetailOwnerComponent } from "../../component/item-detail-owner/item-detail-owner.component";
@Component({
  selector: 'app-item-detail',
  standalone: true,
  imports: [
    BrnSelectImports,
    HlmSelectImports,
    HlmCardContentDirective,
    HlmCardDescriptionDirective,
    HlmCardDirective,
    HlmCardFooterDirective,
    HlmCardHeaderDirective,
    HlmCardTitleDirective,
    FormsModule,
    HlmInputDirective,
    HlmLabelDirective,
    HlmScrollAreaComponent,
    HlmSeparatorDirective,
    NgForOf,
    DatePipe,
    HlmButtonDirective,
    HlmButtonDirective,
    NgIf,
    HlmAlertDirective,
    HlmIconComponent,
    HlmAlertIconDirective,
    HlmAlertTitleDirective,
    HlmAlertDescriptionDirective,
    ItemDetailHistoryComponent,
    ItemDetailOwnerComponent
],
  providers: [provideIcons({ lucideAlertTriangle })],
  templateUrl: './item-detail.component.html',
  styleUrl: './item-detail.component.scss'
})
export class ItemDetailComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm | undefined
  owner = signal<Ownership | undefined>(undefined)
  item: Item = {} as Item
  itemHistory: ItemHistory[] = []
  site: Site = {} as Site
  isDisabled: boolean = false

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {

      $event.returnValue = true
    }
  }
  constructor(private route: ActivatedRoute, private authenticationService: AuthenticationService, private itemService: ItemService) {
    this.route.paramMap.subscribe((param: ParamMap) => {
      this.authenticationService.currentUser$.pipe(take(1)).subscribe(auth => {

        if (auth) {
          this.getItemDetail(+param.get('id')!, auth.user.site.id)
        }
      });
    })
  }
  ngOnInit(): void {

  }


  getItemDetail(id: number, siteId: number) {
    this.itemService.getItemById(id, siteId).subscribe(res => {
      this.owner.set(res)
      this.item = res.item
      this.itemHistory = res.itemHistories
      this.site = res.site
      console.log(res)
    })

  }
  canEditOnClick() {
    if (this.isDisabled) this.updateDetail()
    this.isDisabled = true;
  }
  updateDetail() {

    this.itemService.updateItem(this.item).subscribe((_) => {
      this.editForm?.reset(this.item)
      this.isDisabled = false

      toast.success('Successful', {
        description: 'Item successfully updated',

      })
    })
  }

  history = [
    { status: 'Received', description: 'All items received from supplier', date: '01 Jan 0001' },
  ];


}
