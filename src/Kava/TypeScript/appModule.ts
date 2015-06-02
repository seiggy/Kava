/// <reference path="../../../typings/angularjs/angular.d.ts"/>

"use strict";

var app = angular.module("app", ["kvFramework"]);

app.controller("BlogRollController", ["$scope", $scope => {
    $scope.posts = [
        {
            'month': "May",
            'day': "25",
            'fav_count': 55,
            'post_url': "/posts/banana-monkey.html",
            'title': "Sample Blog Post",
            'stinger': "Post short description goes here [...]",
            'username': "zway",
            'comment_count': 24,
            'tags': [
                "Adventure", "RPG", "Shooter", "Strategy"
            ]
        },
        {
            'month': "May",
            'day': "26",
            'fav_count': 15,
            'post_url': "/posts/apple-car.html",
            'title': "Another Sample Blog Post",
            'stinger': "Another Post short description goes here [...]",
            'username': "zway",
            'comment_count': 14,
            'tags': [
                "Adventure", "Shooter", "Strategy"
            ]
        }
    ];
}]);