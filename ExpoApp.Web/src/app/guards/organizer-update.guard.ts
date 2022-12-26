import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { ExpoService } from '../services/Expo/Expo.service';
import { LoginService } from '../services/login/login.service';

@Injectable({
  providedIn: 'root'
})
export class OrganizerUpdateGuard implements CanActivate {
  constructor(private expoService: ExpoService, private loginService : LoginService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let expoId = route.params["id"];
      let username = this.loginService.GetOrganiser();

      return this.expoService.CanEditExpo(username, expoId).pipe(map((res:any) => {
        return res.data;
      }));
  }
  
}
