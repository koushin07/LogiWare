import {Component, inject, input, OnInit, signal, TrackByFunction} from '@angular/core';
import {Shipment, ShipmentDirection} from "../../../_model/shipment";
import {DatePipe, NgClass, NgForOf, NgIf} from "@angular/common";
import {ShipmentCardComponent} from "../../shipment-card/shipment-card.component";
import {provideIcons} from "@ng-icons/core";
import {lucideArrowUpDown, lucideCheck, lucideChevronRight, lucideGripVertical, lucidePlus} from "@ng-icons/lucide";
import {HlmScrollAreaComponent} from "@spartan-ng/ui-scrollarea-helm";
import {HlmIconComponent} from "@spartan-ng/ui-icon-helm";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ShipmentService} from "../../../_services/shipment.service";
import {debounceTime} from "rxjs";
import {BrnSelectComponent, BrnSelectImports} from "@spartan-ng/ui-select-brain";
import {RouterLink} from "@angular/router";
import {BrnAlertDialogTriggerDirective} from "@spartan-ng/ui-alertdialog-brain";
import {
  BrnDialogContentDirective, BrnDialogDescriptionDirective,
  BrnDialogTitleDirective,
  BrnDialogTriggerDirective
} from "@spartan-ng/ui-dialog-brain";
import {BrnTableComponent} from "@spartan-ng/ui-table-brain";
import {BrnMenuTriggerDirective} from "@spartan-ng/ui-menu-brain";
import {
  HlmSelectContentDirective,
  HlmSelectImports,
  HlmSelectOptionComponent,
  HlmSelectValueDirective
} from "@spartan-ng/ui-select-helm";
import {HlmButtonDirective} from "@spartan-ng/ui-button-helm";
import {
  HlmAlertDialogActionButtonDirective,
  HlmAlertDialogCancelButtonDirective,
  HlmAlertDialogComponent,
  HlmAlertDialogContentComponent, HlmAlertDialogDescriptionDirective, HlmAlertDialogFooterComponent,
  HlmAlertDialogHeaderComponent, HlmAlertDialogOverlayDirective, HlmAlertDialogTitleDirective
} from "@spartan-ng/ui-alertdialog-helm";
import {
  HlmDialogComponent,
  HlmDialogContentComponent,
  HlmDialogFooterComponent,
  HlmDialogHeaderComponent,
  HlmDialogService
} from "@spartan-ng/ui-dialog-helm";
import {
  HlmMenuComponent,
  HlmMenuGroupComponent, HlmMenuItemDirective,
  HlmMenuLabelComponent,
  HlmMenuSeparatorComponent
} from "@spartan-ng/ui-menu-helm";
import {HlmInputDirective} from "@spartan-ng/ui-input-helm";
import { CancelShipmentDialogComponent } from '../../cancel-shipment-dialog/cancel-shipment-dialog.component';
import { ClipboardService } from '../../../_services/clipboard.service';

@Component({
  selector: 'app-out-bound-tab',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    HlmSelectImports,
    BrnSelectImports,
    HlmScrollAreaComponent,
    ShipmentCardComponent,
    NgForOf,
    NgIf,
    NgClass,
    FormsModule,
    DatePipe,
    RouterLink,
    HlmIconComponent,
    HlmAlertDialogComponent,
    BrnAlertDialogTriggerDirective,
    HlmAlertDialogContentComponent,
    HlmAlertDialogHeaderComponent,
    HlmAlertDialogTitleDirective,
    HlmAlertDialogDescriptionDirective,
    HlmAlertDialogFooterComponent,
    HlmAlertDialogCancelButtonDirective,
    HlmAlertDialogActionButtonDirective,
    HlmAlertDialogOverlayDirective,
    HlmDialogComponent,
    BrnDialogTriggerDirective,
    HlmDialogContentComponent,
    BrnDialogContentDirective,
    HlmDialogHeaderComponent,
    BrnDialogTitleDirective,
    BrnDialogDescriptionDirective,
    HlmDialogFooterComponent,
    BrnTableComponent,
    HlmButtonDirective,
    BrnMenuTriggerDirective,
    HlmMenuComponent,
    HlmMenuLabelComponent,
    HlmMenuGroupComponent,
    HlmMenuSeparatorComponent,
    HlmMenuItemDirective,
    HlmInputDirective,
    BrnSelectComponent,
    HlmSelectValueDirective,
    HlmSelectContentDirective,
    HlmSelectOptionComponent,

  ],
  providers: [provideIcons({lucideChevronRight, lucidePlus, lucideArrowUpDown, lucideGripVertical, lucideCheck })],
  templateUrl: './out-bound-tab.component.html',
  styleUrl: './out-bound-tab.component.scss'
})
export class OutBoundTabComponent implements OnInit{
  private readonly _hlmDialogService = inject(HlmDialogService);

  shipments = signal<Shipment[]>([])
  filteredShipments = signal<Shipment[]>([])
  private shipmentService = inject(ShipmentService)
  private fb = inject(FormBuilder)
  clipboardService = inject(ClipboardService)

  selectedShipment: Shipment | null = null;
  toBeReceivedCount: number = 0;
  receivedCount: number = 0;
  totalItemsShip: number = 0;
  chartData: { name: string; value: number }[] = [];
  maxChartValue: number = 0;
  showReceiveModal: boolean = false;
  searchForm!: FormGroup;
  searchTypes = [
    { label: 'Code', value: 'code' },
    { label: 'Status', value: 'status' },
    { label: 'Site', value: 'site' },
  ];

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      searchType: [this.searchTypes[0].value],
      searchQuery: [''],
    });
    this.shipmentService.getShipmentDirection("OUTBOUND").subscribe(res=>{
      console.log(res)
      this.shipments.set(res)
      this.filteredShipments.set(res)
      this.calculateSummary()
      this.prepareChartData()
    })
    this.searchForm.get('searchQuery')?.valueChanges
      .pipe(debounceTime(300)) // Adjust the debounce time as needed
      .subscribe(() => {
        this.onSearch();
      });

  }
  onSearch(): void {
    const { searchType, searchQuery } = this.searchForm.value;
    let shipments = this.shipments();
    if (searchQuery) {
      // Fetch the original list of shipments
      const queryLower = searchQuery.toLowerCase();
      // Perform the search logic here
      console.log(`Searching for ${searchQuery} by ${searchType}`);
      switch (searchType) {
        case 'code':
          shipments = shipments.filter(s => s.shipmentCode.toLowerCase().includes(queryLower));
          break;
        case 'status':
          shipments = shipments.filter(s =>s.status.toLowerCase().includes(queryLower));
          break;
        case 'site':

          shipments = shipments.filter(s => s.site.name.toLowerCase().includes(queryLower));
          break;
        default:
          this.filteredShipments.set(shipments);
          break;
      }

      // Update the observable or reactive variable with the filtered shipments
      this.filteredShipments.set(shipments);
    }else{
      this.filteredShipments.set(shipments);
    }
  }
  calculateSummary() {
    this.toBeReceivedCount = this.shipments().filter(s => s.status === 'Shipped' || s.status === "Partial").length;
    this.receivedCount = this.shipments().filter(s => s.status === 'Received').length;
    this.totalItemsShip = this.shipments()
    .reduce((total, ship) => total + ship.shipmentItems.reduce((sum, item)=>sum+item.quantity!,0) ,0)

    // .reduce((sum, s) => sum + s.shipmentItems.reduce((itemSum, item) => itemSum + (item.shipmentReceives.reduce((total, sr)=>total + sr.quantityReceived,0)), 0), 0);
  }
  selectShipment(shipment: Shipment) {
    this.selectedShipment = shipment;
  }
  prepareChartData() {
    this.chartData = [
      { name: 'To Be Received', value: this.toBeReceivedCount },
      { name: 'Received', value: this.receivedCount },
    ];
    this.maxChartValue = Math.max(...this.chartData.map(item => item.value));
  }
  openReceiveModal(shipment: Shipment) {
    this.selectedShipment = shipment;
    this.showReceiveModal = true;
  }

  closeReceiveModal() {
    this.showReceiveModal = false;
  }
  receiveShipment() {
    if (this.selectedShipment) {
      let allReceived = true;
      let anyReceived = false;

      for (const shipmentItem of this.selectedShipment.shipmentItems) {
        // Check if any quantity has been received across all `shipmentReceive` items
        if (shipmentItem.shipmentReceives?.some(sr => sr.quantityReceived > 0)) {
          anyReceived = true;
        }

        // Calculate total received quantity for comparison
        const totalReceived = shipmentItem.shipmentReceives?.reduce(
          (sum, sr) => sum + (sr.quantityReceived || 0),
          0
        ) || 0;

        // Check if the total received quantity is less than the expected quantity
        if (totalReceived < shipmentItem.quantity!) {
          allReceived = false;
        }
      }


      if (allReceived) {
        this.selectedShipment.status = 'Received';
      } else if (anyReceived) {
        this.selectedShipment.status = 'Partial';
      }

      this.selectedShipment.statusUpdate = new Date().toLocaleString();
      this.calculateSummary();
      this.prepareChartData();
      this.closeReceiveModal();
    }
  }

  getStatusClass(status: Shipment['status']): string {
    return this.shipmentService.getStatusClass(status)

  }

  protected readonly _trackBy: TrackByFunction<Shipment> = (_: number, p: Shipment) => p.shipmentCode;

  get totalQuantityReceived(): number {
    return this.selectedShipment?.shipmentItems
      ? this.selectedShipment.shipmentItems.reduce(
          (total, si) =>
            total +
            (si.shipmentReceives
              ? si.shipmentReceives.reduce((sum, sr) => sum + (sr.quantityReceived || 0), 0)
              : 0),
          0
        )
      : 0;
  }
  cancelShipmentDialog(shipment: Shipment) {
    const dialogRef = this._hlmDialogService.open(CancelShipmentDialogComponent, {
      context: {
          shipment: shipment,
      },

  });

  dialogRef.closed$.subscribe((user) => {
      if (user) {
          console.log('Selected user:', user);
      }
  });
    }
}
