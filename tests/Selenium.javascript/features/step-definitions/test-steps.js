'use strict';

var webdriver = require('selenium-webdriver'),
    By = webdriver.By,
    until = webdriver.until;

var defaultTimeout = 18000;

var endpointConfigFile = './endpoint.config.js';
if (process && process.env && process.env.ENDPOINT_CONFIG_FILE) {
    endpointConfigFile = './' + process.env.ENDPOINT_CONFIG_FILE;
}

var endpoint = require(endpointConfigFile).endpointConfig;

const { When, Then } = require("cucumber");
//const { expect } = require("chai");


When('I navigate to site', function () {
    return this.driver.get(endpoint.url);
});


When('I have the site', function () {
    return this.driver.get(endpoint.url);
});

When('I login as a user {string} with password {string}', async function (username, password) {
    await this.driver.wait(until.elementLocated(By.id("login-link")), defaultTimeout).click();
    await this.driver.wait(until.elementLocated(By.id("Email")), defaultTimeout).sendKeys(username + '\n');
    await this.driver.wait(until.elementLocated(By.id("Password")), defaultTimeout).sendKeys(password + '\n');
    //this.driver.wait(until.elementLocated(By.xpath("//input[@value = 'Log in']")), defaultTimeout).click();
});

When('I navigate to Oil products', async function () {
    // return this.driver.wait(until.elementLocated(By.xpath("//ul[@class='dropdown-menu']/li/a[contains(text(), 'Oil')]")), defaultTimeout).click();
    await this.driver.wait(until.elementLocated(By.partialLinkText('Oil')), defaultTimeout).click();
});


Then('I search for {string}', function (phrase) {
    return this.driver.wait(until.elementLocated(By.name("q")), defaultTimeout).sendKeys(phrase + '\n');
});

Then('I should see on title {string}', function (segment) {
    return this.driver.wait(until.titleContains(segment), defaultTimeout);
});

Then('I should see {string} product', function (product) {
    return this.driver.wait(until.elementLocated(By.xpath("//a[contains(@title, '" + product + "')]")), defaultTimeout);
});
