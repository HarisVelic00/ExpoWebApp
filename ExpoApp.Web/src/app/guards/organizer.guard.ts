import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { LoginService } from '../services/login/login.service';
import { OrganizerService } from '../services/organizer/organizer.service';

@Injectable({
  providedIn: 'root'
})
export class OrganizerGuard implements CanActivate {
  constructor(private loginService: LoginService, private organizerService: OrganizerService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let username = this.loginService.GetOrganiser();

    return this.organizerService.IsOrganizer(username).pipe(map((response: any) => {
      return response.data;
    }));
  }
}
