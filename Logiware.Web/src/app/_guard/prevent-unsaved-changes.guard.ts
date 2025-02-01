import { CanDeactivateFn } from '@angular/router';

export const preventUnsavedChangesGuard: CanDeactivateFn<unknown> = (component) => {

  // @ts-ignore
  if(component.editForm?.dirty){
     if( !confirm("You have made changes, Any Unsaved changes will be lost")){
       return false
     }
  }
  return true;
};
