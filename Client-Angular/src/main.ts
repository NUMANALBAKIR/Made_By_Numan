import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
];

export function getBaseUrl(): string {
  return document.getElementsByTagName('base')[0].href;
}