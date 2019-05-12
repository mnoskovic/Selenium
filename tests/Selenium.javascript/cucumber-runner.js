'use strict';

var child_process = require('child_process');

//var args = ['./node_modules/cucumber/bin/cucumber.js', '--tags=~@ignore'];

var args = ['./node_modules/cucumber/bin/cucumber.js'];

var env = Object.create(process.env);
args.push('--format=pretty');

var error = false;

var result = child_process.spawnSync('node', args, {
    env: env,
    cwd: process.cwd(),
    stdio: [0, 1, 2],
    encoding: 'utf-8'
});

if (result.status !== 0) {
    error = true;
}

if (error) {
    throw 'Some of the tests failed!';
}