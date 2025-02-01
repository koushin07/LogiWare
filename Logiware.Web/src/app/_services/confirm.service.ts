import { Injectable } from '@angular/core';
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  confirm(
    title = 'confirmation',
    message = 'Are you sure you want to do this',
    btnOkTxt = 'OK',
    btnCancelTxt = 'Cancel',
  ): Observable<boolean> {
    const config = {
      initialState: {
        title,
        message,
        btnOkTxt,
        btnCancelTxt
      }

    }
    this.bsModalRef = this.modalService.show(ConfirmDialogComponent, config);

  }
}
