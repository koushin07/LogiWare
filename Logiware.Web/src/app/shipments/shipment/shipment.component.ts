import {Component, computed, effect, inject, OnInit, signal, ViewChild} from '@angular/core';
import {
  HlmTabsComponent,
  HlmTabsContentDirective,
  HlmTabsListComponent,
  HlmTabsTriggerDirective
} from "@spartan-ng/ui-tabs-helm";
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective, HlmCardFooterDirective,
  HlmCardHeaderDirective, HlmCardTitleDirective
} from "@spartan-ng/ui-card-helm";
import {HlmScrollAreaComponent} from "@spartan-ng/ui-scrollarea-helm";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {HlmSeparatorDirective} from "@spartan-ng/ui-separator-helm";
import {HlmIconComponent} from "@spartan-ng/ui-icon-helm";
import {provideIcons} from "@ng-icons/core";
import {lucideChevronRight, lucideInfo, lucideMoreVertical, lucidePlus} from "@ng-icons/lucide";
import {HlmButtonDirective} from "@spartan-ng/ui-button-helm";
import {BrnMenuTriggerDirective} from "@spartan-ng/ui-menu-brain";
import {
  HlmMenuComponent, HlmMenuGroupComponent,
  HlmMenuItemDirective,
  HlmMenuLabelComponent,
  HlmMenuSeparatorComponent
} from "@spartan-ng/ui-menu-helm";
import {HlmSwitchComponent} from "@spartan-ng/ui-switch-helm";
import {BrnSeparatorComponent} from "@spartan-ng/ui-separator-brain";
import {ShipmentService} from "../../_services/shipment.service";
import {Shipment, ShipmentDirection} from "../../_model/shipment";
import {FormsModule} from "@angular/forms";

import {HlmInputDirective} from "@spartan-ng/ui-input-helm";
import {ActivatedRoute, Params, Router, RouterLink} from "@angular/router";
import {ShipmentCardComponent} from "../../component/shipment-card/shipment-card.component";
import {ToReceiveTabComponent} from "../../component/tabs/to-receive-tab/to-receive-tab.component";
import {AuthenticationService} from "../../_services/authentication.service";
import {InBoundTabComponent} from "../../component/tabs/in-bound-tab/in-bound-tab.component";
import {OutBoundTabComponent} from "../../component/tabs/out-bound-tab/out-bound-tab.component";

@Component({
  selector: 'app-shipment',
  standalone: true,
  imports: [
    HlmTabsComponent,
    HlmTabsListComponent,
    HlmTabsTriggerDirective,
    HlmCardHeaderDirective,
    HlmScrollAreaComponent,
    NgForOf,
    HlmSeparatorDirective,
    BrnMenuTriggerDirective,
    HlmMenuComponent,
    HlmMenuLabelComponent,
    HlmMenuGroupComponent,
    HlmMenuItemDirective,
    HlmCardDirective,
    HlmCardTitleDirective,
    HlmCardFooterDirective,
    HlmCardContentDirective,
    HlmSwitchComponent,
    HlmTabsContentDirective,
    DatePipe,
    HlmMenuSeparatorComponent,
    BrnSeparatorComponent,
    HlmCardDescriptionDirective,
    FormsModule,
    NgIf,
    HlmInputDirective,
    HlmButtonDirective,
    HlmIconComponent,
    RouterLink,
    ShipmentCardComponent,
    ToReceiveTabComponent,
    HlmTabsComponent,
    InBoundTabComponent,
    OutBoundTabComponent,

  ],
  providers: [provideIcons({lucideChevronRight, lucidePlus })],
  templateUrl: './shipment.component.html',
  styleUrl: './shipment.component.scss'
})
export class ShipmentComponent implements OnInit{
  private authService = inject(AuthenticationService);
  private shipmentService = inject(ShipmentService)
  private route = inject(ActivatedRoute)
  private router = inject(Router)


  activeTab = ''

  ngOnInit(): void {

      this.route.queryParams.subscribe((params: Params)=>{
        const tab = params['tab'];
        if (tab) {
          this.selectTab(tab); // Activate the tab based on query param
        }
      })
    }

    selectTab(tab: string){
      this.activeTab = tab
      switch (this.activeTab){
        case('inbound') :

          break;
        case('outbound'):

          break;
        default:
          this.router.navigateByUrl('/shipment')
      }

      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: { tab: tab },
        queryParamsHandling: 'merge', // Merge with other query params if any
      });
    }






}
