
'use strict';

var cucumber = require('cucumber');
var webdriver = require('selenium-webdriver'),
    By = webdriver.By,
    until = webdriver.until;

var defaultTimeout = 18000;

var endpointConfigFile = './endpoint.config.js';
if (process && process.env && process.env.ENDPOINT_CONFIG_FILE) {
    endpointConfigFile = './' + process.env.ENDPOINT_CONFIG_FILE;
}

var endpoint = require(endpointConfigFile).endpointConfig;

module.exports = function () {

    this.When(/^I navigate to site$/, function () {
        return this.driver.get(endpoint.url);
    });


    this.When(/^I have the site$/, function () {
        return this.driver.get(endpoint.url);
    });

    this.When(/^I login as a user "([^"]*)" with password "([^"]*)"$/, function (username, password) {
        this.driver.wait(until.elementLocated(By.id("login-link")), defaultTimeout).click();
        this.driver.wait(until.elementLocated(By.id("Email")), defaultTimeout).sendKeys(username + '\n');
        this.driver.wait(until.elementLocated(By.id("Password")), defaultTimeout).sendKeys(password + '\n');
        //this.driver.wait(until.elementLocated(By.xpath("//input[@value = 'Log in']")), defaultTimeout).click();
    });

    this.When(/^I navigate to Oil products$/, function () {
        // return this.driver.wait(until.elementLocated(By.xpath("//ul[@class='dropdown-menu']/li/a[contains(text(), 'Oil')]")), defaultTimeout).click();
        return this.driver.wait(until.elementLocated(By.partialLinkText('Oil')), defaultTimeout).click();
    });


    this.Then(/^I search for "([^"]*)"$/, function (phrase) {
        this.driver.wait(until.elementLocated(By.name("q")), defaultTimeout).sendKeys(phrase + '\n');
    });

    this.Then(/^I should see on title "([^"]*)"$/, function (phrase) {
        return this.driver.wait(until.titleContains(phrase), defaultTimeout);
    });

    this.Then(/^I should see "([^"]*)" product$/, function (product) {
        return this.driver.wait(until.elementLocated(By.xpath("//a[contains(@title, '" + product + "')]")), defaultTimeout);
    });

};
