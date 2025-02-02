import { Component, computed, signal } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  AsyncPipe,
  NgClass,
  NgForOf,
  NgIf,
  NgSwitch,
  NgSwitchCase,
} from '@angular/common';
import {
  CdkDragDrop,
  CdkDropList,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { HlmScrollAreaComponent } from '@spartan-ng/ui-scrollarea-helm';
import { ItemService } from '../../_services/item.service';
import { AuthenticationService } from '../../_services/authentication.service';
import { map, Observable, startWith, take } from 'rxjs';
import { Authenticated } from '../../_model/authenticated';

import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { HlmIconComponent } from '@spartan-ng/ui-icon-helm';
import { lucideLoader2, lucidePlus } from '@ng-icons/lucide';
import { provideIcons } from '@ng-icons/core';
import {
  HlmAccordionContentDirective,
  HlmAccordionDirective,
  HlmAccordionIconDirective,
  HlmAccordionItemDirective,
  HlmAccordionTriggerDirective,
} from '@spartan-ng/ui-accordion-helm';
import { BrnSelectImports } from '@spartan-ng/ui-select-brain';
import { HlmSelectImports } from '@spartan-ng/ui-select-helm';
import { Item } from '../../_model/item';
import { CreateShipment, Shipment } from '../../_model/shipment';
import { ShipmentItem } from '../../_model/shipmentItem';
import { Site } from '../../_model/site';
import { Personnel } from '../../_model/personnel';
import { PersonnelService } from '../../_services/personnel.service';
import { SiteService } from '../../_services/site.service';
import { HlmSpinnerComponent } from '@spartan-ng/ui-spinner-helm';
import { toast } from 'ngx-sonner';
import { ShipmentService } from '../../_services/shipment.service';
import { NotificationService } from '../../_services/notification.service';
import { Ownership } from '../../_model/ownership';
@Component({
  selector: 'app-create-shipment',
  standalone: true,
  imports: [
    NgClass,
    ReactiveFormsModule,
    NgSwitch,
    NgForOf,
    NgSwitchCase,
    NgIf,
    CdkDropList,
    HlmScrollAreaComponent,
    HlmScrollAreaComponent,
    HlmButtonDirective,
    HlmIconComponent,
    HlmAccordionDirective,
    HlmAccordionIconDirective,
    HlmAccordionItemDirective,
    HlmAccordionTriggerDirective,
    HlmAccordionContentDirective,
    AsyncPipe,
    FormsModule,
    BrnSelectImports,
    HlmSelectImports,
    HlmSpinnerComponent,
    HlmButtonDirective,
    HlmButtonDirective,
    HlmButtonDirective,
    HlmButtonDirective,
    HlmButtonDirective,
  ],
  providers: [provideIcons({ lucidePlus, lucideLoader2 })],
  templateUrl: './create-shipment.component.html',
  styleUrl: './create-shipment.component.scss',
})
export class CreateShipmentComponent {
  selectedItem = signal<Item>({} as Item);
  selectedQuantity = signal<Number>(0)
  quantity = signal<number | null>(null);
  availableItems: Ownership[] = [];
  filteredItems: Ownership[] = [];
  isSubmissionValid = true;

  selectedSite: Site = {} as Site;
  filteredSites: Site[] = [];
  availableSite: Site[] = [];
  checkOut = signal<CreateShipment>({
    shipmentItem: [],
  });
  drivers: Personnel[] = [];
  authorization = signal<string>('');
  isAuthorize = false;
  authorizeLoadingState = false;
  validationError: validationError[] = [];

  constructor(
    private itemService: ItemService,
    private personnelService: PersonnelService,
    private authService: AuthenticationService,
    private siteService: SiteService,
    private shipmentService: ShipmentService,
    private notificationService: NotificationService
  ) {
    itemService.getOwner(authService.site().id).subscribe({
      next: (res) => {
        this.availableItems = res;
        // (this.availableItems = {...res.map(o=>o.item), })
        console.log("items below")
        console.log(res)
      },
    });
    personnelService.getDriverPersonnel(authService.site().id).subscribe((res) => {
      this.drivers = res;
    });

    siteService.getSiteExcept(authService.site().id).subscribe((res) => {
      this.availableSite = res;
      console.log(this.availableSite);
    });

    this.checkOut.set({ ...this.checkOut(), siteId: authService.site().id });
  }

  filterItems(value: string) {
    const filterValue = value.toLowerCase();
    this.filteredItems = this.availableItems.filter((owner) =>
      owner.item.name.toLowerCase().includes(filterValue)
    );
  }

  filterSites(value: string) {
    const filterValue = value.toLowerCase();
    this.filteredSites = this.availableSite.filter((site) =>
      site.name.toLowerCase().includes(filterValue)
    );
    console.log(this.filteredSites);
  }

  displayItem(item: Item, quantity: number) {
    this.selectedQuantity.set(quantity)
    this.selectedItem.set(item);
    this.filteredItems = [];
  }
  displaySite(site: Site) {
    const newCheckOut: CreateShipment = this.checkOut();

    newCheckOut.destinationSiteId = site.id;
    this.checkOut.set(newCheckOut);
    this.selectedSite = site;
    this.filteredSites = [];
  }
  addItem() {
    this.checkOut.set({
      ...this.checkOut(),
      shipmentItem: [
        ...(this.checkOut().shipmentItem ?? []),
        {
          ownership: {
            item: this.selectedItem(),
            quantity: this.quantity()!,
            site: this.authService.site(),
          },
        } as ShipmentItem,
      ],
    });

    console.log(this.checkOut());
    console.log(this.checkOut());
    this.selectedItem.set({} as Item);
    this.filteredItems = this.filteredItems.map(owner => {
      if (this.selectedItem().id === owner.item.id) {
          // Create a new object with the updated quantity
          return {
              ...owner,
              quantity: owner.quantity - this.quantity()!
          };
      }
      return owner;
  });

    // this.filteredItems = [...this.filteredItems.a]
    this.selectedQuantity.set(0)

    this.quantity.set(null);
  }

  adjustQuantity(adjustment: number, index: number) {
    const shipmentItem: ShipmentItem | undefined =
      this.checkOut().shipmentItem.at(index);
    if (shipmentItem) {
      shipmentItem.ownership.quantity =
        shipmentItem.ownership.quantity + adjustment;
    }
  }

  isItemSelected = computed(() => {
    const checkquantity = this.quantity() != null && this.quantity()! > 0;

    const selectedItem = this.selectedItem(); // Assuming `selectedItem` is a signal
    const shipmentItems = this.checkOut().shipmentItem; // Also reactive
    console.log(shipmentItems);
    const excessquantity = this.quantity()! < selectedItem.quantity;
    if (checkquantity && shipmentItems.length == 0 && !excessquantity) {
      console.log('shipmentitem has no lenght');
      return false;
    }

    const isItemInShipment = shipmentItems.some(
      (sp) => sp.ownership.item.id === selectedItem.id
    );
    console.log(isItemInShipment);

    const checkSelectedItem = this.availableItems.some(
      (o) => o.item.id == selectedItem.id
    );
    console.log(` quantity: ${this.quantity}`);
    console.log(`check quantity: ${checkquantity}`);
    console.log(
      `check if selected Item is part of avialble item: ${checkSelectedItem}`
    );
    console.log(
      `check if the selected item is already select: ${!isItemInShipment}`
    );
    console.log(`check if the quantity is too much: ${excessquantity}`);

    const validation =
      !isItemInShipment && checkquantity && excessquantity && checkSelectedItem;
    console.log(validation);
    return validation;
  });

  onSubmit() {
    console.log(this.checkOut());
    this.validateCheckout();
    if (!this.isSubmissionValid) return;
    this.shipmentService.sendShipment(this.checkOut()).subscribe({
      next: () => {
        toast.success('shipment checkout', {
          description: 'success',
        });
        window.location.reload
        this.notificationService.sendNotification({ message: "shipment in-bound received", receiverId: this.checkOut().destinationSiteId!, senderId: this.checkOut().siteId! })
      },
    });
  }

  authorizeCheckOut() {
    if (this.authorization().length <= 0) return
    this.authorizeLoadingState = true;
    console.log(this.authorization());
    this.personnelService
      .getAuthorizePersonnel(this.authorization())
      .subscribe({
        next: (res) => {
          setTimeout(() => {
            toast.success('Authorize Completed', {
              description: `authorize by ${res.lastName}, ${res.firstName}`,
            });
            this.authorizeLoadingState = false;
            this.isAuthorize = true;
            this.checkOut.set({ ...this.checkOut(), authorizedBy: res.id });
          }, 2000);
        },
        error: (err) => {

          console.log(err);
          this.authorizeLoadingState = false;
        },
      });
  }

  removeItem(id: number) {
    console.log('remove click');

    const newCheckout = this.checkOut();

    newCheckout.shipmentItem = newCheckout.shipmentItem.filter(
      (i) => i.id !== id
    );

    this.checkOut.set(newCheckout);
  }
  private validateCheckout() {
    const newCheckout = this.checkOut();

    for (const key in newCheckout) {
      if (newCheckout.hasOwnProperty(key)) {
        const typedKey = key as keyof CreateShipment;
        const value = newCheckout[typedKey];

        // Perform validation based on the property
        switch (key) {
          case 'siteId':
            if (typeof value == 'number' && value === 0) {
              console.log(`${key} is not valid`);
              this.validationError.push({
                name: 'site',
                message: 'Please select site',
              });
              this.isSubmissionValid = false;
            }
            break;
          case 'driverId':
            if (typeof value == 'number' && value === 0) {
              console.log(`${key} is not valid`);
              this.validationError.push({
                name: 'driver',
                message: 'Please select driver',
              });
              this.isSubmissionValid = false;
            }
            break;

          case 'shipmentItem':
            if (Array.isArray(value) && value.length === 0) {
              console.log('No items in the shipment');
              this.validationError.push({
                name: 'shipmentItem',
                message: 'Please add item',
              });

              this.isSubmissionValid = false;
            }
            break;
          case 'ownerSiteId':
            if (Array.isArray(value) && value.length === 0) {
              console.log('No items in the shipment');
              this.validationError.push({
                name: 'ownerSiteId',
                message: 'Please add owner site',
              });
              this.isSubmissionValid = false;
            }
            break;
        }
      }
    }
  }
}

interface validationError {
  name: string;
  message: string;
}
