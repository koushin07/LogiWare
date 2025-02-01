import {Component, computed, effect, OnInit, signal, TrackByFunction} from '@angular/core';

import {HlmCheckboxCheckIconComponent, HlmCheckboxComponent} from '@spartan-ng/ui-checkbox-helm';

import {
  HlmCaptionComponent,
  HlmTableComponent, HlmTableDirective, HlmTableModule,
  HlmTdComponent,
  HlmThComponent,
  HlmTrowComponent,
} from '@spartan-ng/ui-table-helm';
import {ItemService} from "../../_services/item.service";
import {Item} from "../../_model/item";
import {DecimalPipe, NgForOf, TitleCasePipe} from "@angular/common";
import {
  BrnCellDefDirective,
  BrnColumnDefComponent,
  BrnHeaderDefDirective,
  BrnTableComponent, BrnTableModule, PaginatorState,
  useBrnColumnManager
} from "@spartan-ng/ui-table-brain";
import {HlmIconComponent} from "@spartan-ng/ui-icon-helm";
import {provideIcons} from "@ng-icons/core";
import {lucideArrowUpDown, lucideChevronDown, lucideMoreHorizontal, lucidePlus} from "@ng-icons/lucide";
import {toObservable, toSignal} from "@angular/core/rxjs-interop";
import {debounceTime, map, take} from "rxjs";
import {SelectionModel} from "@angular/cdk/collections";
import {HlmInputDirective} from "@spartan-ng/ui-input-helm";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {HlmMenuComponent, HlmMenuItemCheckComponent, HlmMenuModule} from "@spartan-ng/ui-menu-helm";
import {BrnMenuTriggerDirective} from "@spartan-ng/ui-menu-brain";
import {HlmButtonDirective, HlmButtonModule} from "@spartan-ng/ui-button-helm";
import {BrnSelectImports, BrnSelectModule} from "@spartan-ng/ui-select-brain";
import {HlmSelectImports, HlmSelectModule} from "@spartan-ng/ui-select-helm";
import {AuthenticationService} from "../../_services/authentication.service";
import {Authenticated} from "../../_model/authenticated";
import {BrnAlertDialogContentDirective, BrnAlertDialogTriggerDirective} from "@spartan-ng/ui-alertdialog-brain";
import {
  HlmAlertDialogActionButtonDirective,
  HlmAlertDialogCancelButtonDirective,
  HlmAlertDialogComponent,
  HlmAlertDialogContentComponent,
  HlmAlertDialogDescriptionDirective,
  HlmAlertDialogFooterComponent,
  HlmAlertDialogHeaderComponent,
  HlmAlertDialogTitleDirective
} from "@spartan-ng/ui-alertdialog-helm";
import {HlmFormFieldComponent, HlmHintDirective} from "@spartan-ng/ui-formfield-helm";
import {F} from "@angular/cdk/keycodes";
import {Router, RouterLink} from "@angular/router";
import { Ownership } from '../../_model/ownership';
import { ClipboardService } from '../../_services/clipboard.service';
@Component({
  selector: 'app-item',
  standalone: true,
  imports: [
    FormsModule,

    BrnMenuTriggerDirective,
    HlmMenuModule,

    BrnTableModule,
    HlmTableModule,

    HlmButtonModule,

    DecimalPipe,
    TitleCasePipe,
    HlmIconComponent,
    HlmInputDirective,

    HlmCheckboxCheckIconComponent,
    HlmCheckboxComponent,

    BrnSelectModule,
    HlmSelectModule,
    HlmButtonDirective,
    BrnAlertDialogTriggerDirective,
    HlmAlertDialogComponent,
    HlmAlertDialogContentComponent,
    HlmAlertDialogTitleDirective,
    HlmAlertDialogDescriptionDirective,
    HlmAlertDialogHeaderComponent,
    HlmAlertDialogFooterComponent,
    HlmAlertDialogComponent,
    BrnAlertDialogContentDirective,
    HlmAlertDialogContentComponent,
    HlmAlertDialogHeaderComponent,
    HlmAlertDialogTitleDirective,
    HlmAlertDialogDescriptionDirective,
    HlmAlertDialogFooterComponent,
    HlmAlertDialogCancelButtonDirective,
    HlmAlertDialogActionButtonDirective,
    HlmFormFieldComponent,
    HlmInputDirective,
    HlmHintDirective,
    ReactiveFormsModule,
    HlmFormFieldComponent,
    BrnSelectImports,
    HlmSelectImports,
    HlmButtonDirective,
    HlmButtonDirective,
    RouterLink
  ],
  providers: [provideIcons({ lucideChevronDown, lucideMoreHorizontal, lucideArrowUpDown, lucidePlus })],
  templateUrl: './item.component.html',
  styleUrl: './item.component.scss'
})
export class ItemComponent implements OnInit{
  private readonly _items = signal<Item[]>([])
  private auth!: Authenticated;
  itemForm!: FormGroup;
  ownership = signal<Ownership[]>([])


  protected readonly _rawFilterInput = signal('');
  protected readonly _nameFilter = signal('');
  private readonly _debouncedFilter = toSignal(toObservable(this._rawFilterInput).pipe(debounceTime(300)));

  private readonly _displayedIndices = signal({ start: 0, end: 0 });
  protected readonly _availablePageSizes = [5, 10, 20, 10000];
  protected readonly _pageSize = signal(this._availablePageSizes[0]);

  private readonly _selectionModel = new SelectionModel<Ownership>(true);
  protected readonly _isPaymentSelected = (item: Ownership) => this._selectionModel.isSelected(item);
  protected readonly _selected = toSignal(this._selectionModel.changed.pipe(map((change) => change.source.selected)), {
    initialValue: [],
  });

  protected readonly _brnColumnManager = useBrnColumnManager({
    itemCode: { visible: true, label: 'Item Code' },
    name: { visible: true, label: 'Name' },
    description: { visible: true, label: 'Description' },
    category: { visible: true, label: 'Category' },
    quantity: { visible: true, label: 'Quantity' },
  });
  protected readonly _allDisplayedColumns = computed(() => [
    'select',
    ...this._brnColumnManager.displayedColumns(),
    'actions',
  ]);

 /* private readonly _payments = signal(PAYMENT_DATA);*/
  private readonly _filteredPayments = computed(() => {
    const emailFilter = this._nameFilter()?.trim()?.toLowerCase();
    if (emailFilter && emailFilter.length > 0) {
      return this.ownership().filter((u) => u.item.name.toLowerCase().includes(emailFilter));
    }
    return this.ownership();
  });
  private readonly _emailSort = signal<'ASC' | 'DESC' | null>(null);
  private readonly _nameSort = signal<'ASC' | 'DESC' | null>(null);


  constructor(
    private fb: FormBuilder,
    private itemService: ItemService,
    private authService: AuthenticationService,
    private router: Router,
    public clipboardService: ClipboardService
  ) {
    authService.currentUser$.pipe(take(1)).subscribe(auth =>{

      if(auth){

        this.auth = auth
        this.getItems(auth.user.site.id)
      }
    });
    this.itemForm = fb.group({
      quantity: [ [Validators.required ]],
      name: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      siteId: [authService.site().id]
    })
    // needed to sync the debounced filter to the name filter, but being able to override the
    // filter when loading new users without debounce
    effect(() => this._nameFilter.set(this._debouncedFilter() ?? ''), { allowSignalWrites: true });
  }


  protected readonly _filteredSortedPaginatedPayments = computed(() => {
    const sort = this._emailSort();
    const sortName = this._nameSort();
    const start = this._displayedIndices().start;
    const end = this._displayedIndices().end + 1;
    const payments = this._filteredPayments();
    if (!sort) {
      return payments.slice(start, end);
    }
    return [...payments]
      .sort((p1, p2) => (sort === 'ASC' ? 1 : -1) * p1.item.description.localeCompare(p2.item.description))
      .sort((p1, p2) => (sortName === 'ASC' ? 1 : -1) * p1.item.name.localeCompare(p2.item.name))
      .slice(start, end);
  });
  protected readonly _allFilteredPaginatedPaymentsSelected = computed(() =>
    this._filteredSortedPaginatedPayments().every((payment) => this._selected().includes(payment)),
  );
  protected readonly _checkboxState = computed(() => {
    const noneSelected = this._selected().length === 0;
    const allSelectedOrIndeterminate = this._allFilteredPaginatedPaymentsSelected() ? true : 'indeterminate';
    return noneSelected ? false : allSelectedOrIndeterminate;
  });

  protected readonly _trackBy: TrackByFunction<Item> = (_: number, p: Item) => p.id;
  protected readonly _totalElements = computed(() => this._filteredPayments().length);
  protected readonly _onStateChange = ({ startIndex, endIndex }: PaginatorState) =>
    this._displayedIndices.set({ start: startIndex, end: endIndex });



  ngOnInit(): void {

    }

  protected togglePayment(payment: Ownership) {
    this._selectionModel.toggle(payment);
  }

  protected handleHeaderCheckboxChange() {
    const previousCbState = this._checkboxState();
    if (previousCbState === 'indeterminate' || !previousCbState) {
      this._selectionModel.select(...this._filteredSortedPaginatedPayments());
    } else {
      this._selectionModel.deselect(...this._filteredSortedPaginatedPayments());
    }
  }

  protected handleEmailSortChange() {
    const sort = this._emailSort();
    if (sort === 'ASC') {
      this._emailSort.set('DESC');
    } else if (sort === 'DESC') {
      this._emailSort.set(null);
    } else {
      this._emailSort.set('ASC');
    }
  }

  protected handleNameSortChange() {
    const sort = this._nameSort();
    if (sort === 'ASC') {
      this._nameSort.set('DESC');
    } else if (sort === 'DESC') {
      this._nameSort.set(null);
    } else {
      this._nameSort.set('ASC');
    }
  }

  protected getItems( siteId: number){
    this.itemService.getOwner(siteId).subscribe((i) => {
      this.ownership.set(i)

    })
  }

  createItem(ctx: any){
    console.log(this.itemForm.value)
  this.itemService.createItem(this.itemForm).subscribe((item)=>{
    console.log(item)
    console.log("submitted")
    window.location.reload()
    ctx.close()

  })
  }

}
