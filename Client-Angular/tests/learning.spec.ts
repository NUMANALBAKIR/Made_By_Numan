import { chromium, test } from "@playwright/test"

test(
    't1', async () => {
        const browser = await chromium.launch({
            // headless: false
        });
        const context = await browser.newContext();
        const page = await context.newPage(); // new tab

        await page.goto('https://ecommerce-playground.lambdatest.io/');

        // await page.hover("//a[contains(text(),'My account')]");

        await page.hover("//a[@data-toggle='dropdown']//span[contains(.,'My account')]");
        await page.click("'Login'");

        await page.fill("input[name='email']", "koushik350@gmail.com");
        await page.fill("input[name='password']", "Pass123$");

        await page.click("input[value='Login']")

        await page.waitForTimeout(5000);


    }
);
