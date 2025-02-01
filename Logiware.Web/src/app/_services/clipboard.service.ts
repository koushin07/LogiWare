import { Injectable } from '@angular/core';
import { Clipboard } from '@angular/cdk/clipboard';
import { toast } from 'ngx-sonner';
@Injectable({
  providedIn: 'root'
})
export class ClipboardService {

  constructor(private clipboard: Clipboard) { }


  copyToText(text: string): void {
    this.clipboard.copy(text);
    console.log(text)
    toast.success("Copy to clipboard")
  }
}
