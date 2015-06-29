/// <reference path="../../../typings/angularjs/angular.d.ts"/>

"use strict";

var month = ["January","February","March","April","May","June","July","August","September","October","November","December"];

angular.module("kvCalendar", [])
    .controller("kvCalendarController", [
        "$scope", $scope => {
            $scope.events = [
                {
                    //cssClass: "static-raid",
                    startsAt: new Date(2015, 6, 12, 18, 0, 0, 0),
                    endsAt: new Date(2015, 6, 12, 20, 0, 0, 0),
                    editable: false,
                    deletable: false,
                    incrementBadgeTotal: true,
                    type: "info",
                    title: "Astrid's Raid Group",
                    Id: 1
                }
            ];

            $scope.calendarView = "month";
            $scope.calendarDay = new Date();
            $scope.calendarTitle = "KoR Event Calendar";
            $scope.calendarMonth = month[(new Date()).getMonth()];
        }
    ]);