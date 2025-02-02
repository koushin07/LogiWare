import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { AuthLayoutComponent } from "./layout/auth-layout/auth-layout.component";
import { HomeComponent } from "./home/home.component";
import { ItemComponent } from "./items/item/item.component";
import { ShipmentComponent } from "./shipments/shipment/shipment.component";
import { ItemDetailComponent } from "./items/item-detail/item-detail.component";
import { preventUnsavedChangesGuard } from "./_guard/prevent-unsaved-changes.guard";
import { CreateShipmentComponent } from "./shipments/create-shipment/create-shipment.component";
import { ShipmentReceiveComponent } from "./shipments/shipment-receive/shipment-receive.component";
import { authenticateGuard } from "./_guard/authenticate.guard";
import { NotFoundComponent } from './fallback/not-found/not-found.component';
import { ProfileComponent } from './site/profile/profile.component';
import { AdminLoginComponent } from './admin/admin-login/admin-login.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { adminGuard } from './_guard/admin.guard';

export const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    pathMatch: 'full'
  },
  {
    path: '',
    component: AuthLayoutComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [authenticateGuard],
    children: [
      {
        path: 'home',
        component: HomeComponent
      },
      {
        path: 'item',
        component: ItemComponent,
      },
      {
        path: 'item/:id',
        component: ItemDetailComponent,
        canDeactivate: [preventUnsavedChangesGuard]
      },
      {
        path: 'shipment',
        component: ShipmentComponent
      },
      {
        path: 'shipment/create',
        component: CreateShipmentComponent
      },
      {
        path: 'shipment/receive/:code',
        component: ShipmentReceiveComponent
      },

      {
        path: "site",
        children: [
          {
            path: "profile",
            component: ProfileComponent
          }
        ]
      }

    ],

  },
  {
    path: "admin",
    children: [
      {
        path: 'login',
        component: AdminLoginComponent

      },
      {
        path: "dashboard",
        component: AdminDashboardComponent,
        canActivate: [adminGuard]

      }
    ]
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];
