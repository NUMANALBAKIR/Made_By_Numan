// import { test, chromium } from '@playwright/test';

// test('search for playwright on google using Edge', async () => {
//   // Launch Microsoft Edge browser
//   const browser = await chromium.launch({
//     channel: 'msedge'
//   });
//   const context = await browser.newContext();
//   const page = await context.newPage();

//   // Go to Google
//   await page.goto('https://www.google.com');

//   await page.waitForTimeout(5000);
//   // Type "playwright" into the search box

//   // // Press 'Enter' to search
//   // await page.press('input[name="q"]', 'Enter');

//   // // Wait for the search results to load
//   // await page.waitForSelector('text=Playwright');

//   // // Verify that the search results contain "playwright"
//   // const title = await page.title();
//   // expect(title).toContain('playwright');

//   // Close the browser
//   // await context.close();
// });



import { test, expect } from '@playwright/test';

test('has title', async ({ page }) => {
  await page.goto('https://playwright.dev/');

  // Expect a title "to contain" a substring.
  await expect(page).toHaveTitle(/Playwright/);
});

test('get started link', async ({ page }) => {
  await page.goto('https://playwright.dev/');

  // Click the get started link.
  await page.getByRole('link', { name: 'Get started' }).click();

  // Expects page to have a heading with the name of Installation.
  await expect(page.getByRole('heading', { name: 'Installation' })).toBeVisible();
});
