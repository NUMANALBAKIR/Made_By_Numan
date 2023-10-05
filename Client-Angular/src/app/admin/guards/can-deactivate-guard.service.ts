import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

/*
  Used in createStudent(T.D.F), editStudent(R.F)
*/

@Injectable({
  providedIn: 'root'
})
export class CanDeactivateGuardService implements CanDeactivate<ICanDeactivate> {

  canDeactivate(component: ICanDeactivate, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot | undefined): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if (component.canLeave == true) {
      return true;
    } else {
      return confirm('Can-Deactivate-Guard says: Changes will be lost. Leave page?');
    }
  }
}


export interface ICanDeactivate {
  canLeave: boolean;
}

