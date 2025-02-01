import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {SidebarComponent} from "../../component/sidebar/sidebar.component";
import {CommonModule, NgClass} from "@angular/common";
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import { HlmAvatarComponent, HlmAvatarFallbackDirective, HlmAvatarImageDirective } from '@spartan-ng/ui-avatar-helm';
import { provideIcons } from '@ng-icons/core';
import { HlmIconComponent } from '@spartan-ng/ui-icon-helm';
import {lucideBaggageClaim, lucideLayoutDashboard, lucidePlus, lucideWarehouse} from "@ng-icons/lucide";
import {HlmTooltipComponent, HlmTooltipTriggerDirective} from "@spartan-ng/ui-tooltip-helm";
import {BrnTooltipContentDirective} from "@spartan-ng/ui-tooltip-brain";
import {AuthenticationService} from "../../_services/authentication.service";
import {Site} from "../../_model/site";
import {
  HlmMenuComponent,
  HlmMenuGroupComponent,
  HlmMenuItemDirective,
  HlmMenuItemIconDirective,
  HlmMenuItemSubIndicatorComponent,
  HlmMenuLabelComponent,
  HlmMenuSeparatorComponent,
  HlmMenuShortcutComponent, HlmSubMenuComponent
} from "@spartan-ng/ui-menu-helm";
import {BrnMenuTriggerDirective} from "@spartan-ng/ui-menu-brain";

import { HlmBadgeDirective } from '@spartan-ng/ui-badge-helm';

@Component({
  selector: 'app-auth-layout',
  standalone: true,
  imports: [
    HlmBadgeDirective,
    SidebarComponent,
    NgClass,
    RouterLink,
    CommonModule,
    HlmAvatarComponent,
    HlmAvatarFallbackDirective,
    HlmAvatarImageDirective,
    RouterOutlet,
    HlmIconComponent,
    HlmIconComponent,
    RouterLinkActive,
    HlmTooltipComponent,
    HlmTooltipTriggerDirective,
    BrnTooltipContentDirective,
    HlmIconComponent,
    HlmMenuComponent,
    HlmMenuGroupComponent,
    HlmMenuItemDirective,
    HlmMenuItemIconDirective,
    HlmMenuItemSubIndicatorComponent,
    HlmMenuLabelComponent,
    HlmMenuSeparatorComponent,
    HlmMenuShortcutComponent,
    HlmSubMenuComponent,
    BrnMenuTriggerDirective,
  ],
  providers: [provideIcons( {lucideLayoutDashboard, lucideWarehouse, lucideBaggageClaim, lucidePlus})],
  templateUrl: './auth-layout.component.html',
  styleUrl: './auth-layout.component.scss'
})
export class AuthLayoutComponent {

  @ViewChild('sidebar') sidebar!: ElementRef;
  @ViewChild('maxSidebar') maxSidebar!: ElementRef;
  @ViewChild('miniSidebar') miniSidebar!: ElementRef;
  @ViewChild('maxToolbar') maxToolbar!: ElementRef;
  @ViewChild('logo') logo!: ElementRef;
  @ViewChild('content') content!: ElementRef;
  @ViewChild('moon') moon!: ElementRef;
  @ViewChild('sun') sun!: ElementRef;
  protected readonly _site: Site = {} as Site

  sidebarItem: sidebarItem[] =[
    {name: 'Home', path: '/home', icon: 'lucideLayoutDashboard'},
    {name: 'Item', path: '/item', icon: 'lucideWarehouse'},
    {name: 'Shipment', path: '/shipment', icon: 'lucideBaggageClaim'}
]

  constructor(private renderer: Renderer2, private authService: AuthenticationService, private router: Router) {
    this._site = authService.site()
  }



  isMiniSidebar = true;  // Flag to toggle sidebar size
  isDarkMode = false;  // Flag to toggle dark/light mode



  setDark(mode: string): void {
    if (mode === 'dark') {
      this.isDarkMode = true;
      this.renderer.addClass(document.documentElement, 'dark');
      this.renderer.addClass(this.moon.nativeElement, 'hidden');
      this.renderer.removeClass(this.sun.nativeElement, 'hidden');
    } else {
      this.isDarkMode = false;
      this.renderer.removeClass(document.documentElement, 'dark');
      this.renderer.addClass(this.sun.nativeElement, 'hidden');
      this.renderer.removeClass(this.moon.nativeElement, 'hidden');
    }

  }

  toggleSidebar(): void {
    this.isMiniSidebar = !this.isMiniSidebar;
    console.log("hit")
    if (this.sidebar.nativeElement.classList.contains('-translate-x-48')) {
      // Max sidebar
      this.renderer.removeClass(this.sidebar.nativeElement, '-translate-x-48');
      this.renderer.addClass(this.sidebar.nativeElement, 'translate-x-none');
      this.renderer.removeClass(this.maxSidebar.nativeElement, 'hidden');
      this.renderer.addClass(this.maxSidebar.nativeElement, 'flex');
      this.renderer.removeClass(this.miniSidebar.nativeElement, 'flex');
      this.renderer.addClass(this.miniSidebar.nativeElement, 'hidden');
      this.renderer.addClass(this.maxToolbar.nativeElement, 'translate-x-0');
      this.renderer.removeClass(this.maxToolbar.nativeElement, 'translate-x-24');
      this.renderer.removeClass(this.maxToolbar.nativeElement, 'scale-x-0');
      this.renderer.removeClass(this.logo.nativeElement, 'ml-12');
      this.renderer.removeClass(this.content.nativeElement, 'ml-12');
      this.renderer.addClass(this.content.nativeElement, 'ml-24');
      this.renderer.addClass(this.content.nativeElement, 'md:ml-60');
    } else {
      // Mini sidebar
      this.renderer.addClass(this.sidebar.nativeElement, '-translate-x-48');
      this.renderer.removeClass(this.sidebar.nativeElement, 'translate-x-none');
      this.renderer.addClass(this.maxSidebar.nativeElement, 'hidden');
      this.renderer.removeClass(this.maxSidebar.nativeElement, 'flex');
      this.renderer.addClass(this.miniSidebar.nativeElement, 'flex');
      this.renderer.removeClass(this.miniSidebar.nativeElement, 'hidden');
      this.renderer.addClass(this.maxToolbar.nativeElement, 'translate-x-24');
      this.renderer.addClass(this.maxToolbar.nativeElement, 'scale-x-0');
      this.renderer.removeClass(this.maxToolbar.nativeElement, 'translate-x-0');
      this.renderer.addClass(this.logo.nativeElement, 'ml-12');
      this.renderer.removeClass(this.content.nativeElement, 'ml-12');
      this.renderer.removeClass(this.content.nativeElement, 'md:ml-60');
      this.renderer.addClass(this.content.nativeElement, 'ml-12');
    }
  }
  logout() {
    this.authService.logoutUser()
    this.router.navigateByUrl('/')
  }
}


export interface sidebarItem{
  icon: string,
  name: string,
  path: string
}
