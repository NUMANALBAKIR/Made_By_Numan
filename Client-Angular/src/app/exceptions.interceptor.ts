import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ExceptionsInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}
  // nstall toastr

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(catchError(
      (r) => {
        console.log(r);
        const code = r.status;

        switch (code) {
          case 404:
            break;

          case 500:
            const nextras: NavigationExtras = {state: {e: r.error}};
            this.router.navigate(['/500', nextras]);
            break;

          default:
            break;
        }


        throw "";
      }

    ));
  }
}
