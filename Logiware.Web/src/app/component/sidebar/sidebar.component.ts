import {Component, ElementRef, Renderer2, ViewChild} from '@angular/core';
import {NgClass} from "@angular/common";

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  @ViewChild('sidebar') sidebar!: ElementRef;
  @ViewChild('maxSidebar') maxSidebar!: ElementRef;
  @ViewChild('miniSidebar') miniSidebar!: ElementRef;
  @ViewChild('maxToolbar') maxToolbar!: ElementRef;
  @ViewChild('logo') logo!: ElementRef;
  @ViewChild('content') content!: ElementRef;
  @ViewChild('moon') moon!: ElementRef;
  @ViewChild('sun') sun!: ElementRef;

  constructor(private renderer: Renderer2) {}


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
      this.renderer.addClass(this.content.nativeElement, 'ml-12');
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
}
