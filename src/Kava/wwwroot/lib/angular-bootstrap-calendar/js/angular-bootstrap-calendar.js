/**
 * angular-bootstrap-calendar - A pure AngularJS bootstrap themed responsive calendar that can display events and has views for year, month, week and day
 * @version v0.10.7
 * @link https://github.com/mattlewis92/angular-bootstrap-calendar
 * @license MIT
 */
(function (window, angular) {
    'use strict';
    angular.module('mwl.calendar', []);
    'use strict';
    angular.module('mwl.calendar').constant('moment', window.moment);
    'use strict';
    angular.module('mwl.calendar').factory('calendarTitle', [
        'moment',
        'calendarConfig',
        function (moment, calendarConfig) {
            function day(currentDay) {
                return moment(currentDay).format(calendarConfig.titleFormats.day);
            }
            function week(currentDay) {
                var weekTitleLabel = calendarConfig.titleFormats.week;
                return weekTitleLabel.replace('{week}', moment(currentDay).week()).replace('{year}', moment(currentDay).format('YYYY'));
            }
            function month(currentDay) {
                return moment(currentDay).format(calendarConfig.titleFormats.month);
            }
            function year(currentDay) {
                return moment(currentDay).format(calendarConfig.titleFormats.year);
            }
            return {
                day: day,
                week: week,
                month: month,
                year: year
            };
        }
    ]);
    'use strict';
    angular.module('mwl.calendar').factory('calendarHelper', [
        'moment',
        'calendarConfig',
        function (moment, calendarConfig) {
            function eventIsInPeriod(eventStart, eventEnd, periodStart, periodEnd) {
                eventStart = moment(eventStart);
                eventEnd = moment(eventEnd);
                periodStart = moment(periodStart);
                periodEnd = moment(periodEnd);
                return eventStart.isAfter(periodStart) && eventStart.isBefore(periodEnd) || eventEnd.isAfter(periodStart) && eventEnd.isBefore(periodEnd) || eventStart.isBefore(periodStart) && eventEnd.isAfter(periodEnd) || eventStart.isSame(periodStart) || eventEnd.isSame(periodEnd);
            }
            function getEventsInPeriod(calendarDate, period, allEvents) {
                var startPeriod = moment(calendarDate).startOf(period);
                var endPeriod = moment(calendarDate).endOf(period);
                return allEvents.filter(function (event) {
                    return eventIsInPeriod(event.startsAt, event.endsAt, startPeriod, endPeriod);
                });
            }
            function getBadgeTotal(events) {
                return events.filter(function (event) {
                    return event.incrementsBadgeTotal !== false;
                }).length;
            }
            function getWeekDayNames() {
                var weekdays = [];
                var count = 0;
                while (count < 7) {
                    weekdays.push(moment().weekday(count++).format(calendarConfig.dateFormats.weekDay));
                }
                return weekdays;
            }
            function filterEventsInPeriod(events, startPeriod, endPeriod) {
                return events.filter(function (event) {
                    return eventIsInPeriod(event.startsAt, event.endsAt, startPeriod, endPeriod);
                });
            }
            function getYearView(events, currentDay) {
                var view = [];
                var eventsInPeriod = getEventsInPeriod(currentDay, 'year', events);
                var month = moment(currentDay).startOf('year');
                var count = 0;
                while (count < 12) {
                    var startPeriod = month.clone();
                    var endPeriod = startPeriod.clone().endOf('month');
                    var periodEvents = filterEventsInPeriod(eventsInPeriod, startPeriod, endPeriod);
                    view.push({
                        label: startPeriod.format(calendarConfig.dateFormats.month),
                        isToday: startPeriod.isSame(moment().startOf('month')),
                        events: periodEvents,
                        date: startPeriod,
                        badgeTotal: getBadgeTotal(periodEvents)
                    });
                    month.add(1, 'month');
                    count++;
                }
                return view;
            }
            function getMonthView(events, currentDay) {
                var eventsInPeriod = getEventsInPeriod(currentDay, 'month', events);
                var startOfMonth = moment(currentDay).startOf('month');
                var day = startOfMonth.clone().startOf('week');
                var endOfMonthView = moment(currentDay).endOf('month').endOf('week');
                var view = [];
                var today = moment().startOf('day');
                while (day.isBefore(endOfMonthView)) {
                    var inMonth = day.month() === moment(currentDay).month();
                    var monthEvents = [];
                    if (inMonth) {
                        monthEvents = filterEventsInPeriod(eventsInPeriod, day, day.clone().endOf('day'));
                    }
                    view.push({
                        label: day.date(),
                        date: day.clone(),
                        inMonth: inMonth,
                        isPast: today.isAfter(day),
                        isToday: today.isSame(day),
                        isFuture: today.isBefore(day),
                        isWeekend: [
                            0,
                            6
                        ].indexOf(day.day()) > -1,
                        events: monthEvents,
                        badgeTotal: getBadgeTotal(monthEvents)
                    });
                    day.add(1, 'day');
                }
                return view;
            }
            function getWeekView(events, currentDay) {
                var startOfWeek = moment(currentDay).startOf('week');
                var endOfWeek = moment(currentDay).endOf('week');
                var dayCounter = startOfWeek.clone();
                var days = [];
                var today = moment().startOf('day');
                while (days.length < 7) {
                    days.push({
                        weekDayLabel: dayCounter.format(calendarConfig.dateFormats.weekDay),
                        date: dayCounter.clone(),
                        dayLabel: dayCounter.format(calendarConfig.dateFormats.day),
                        isPast: dayCounter.isBefore(today),
                        isToday: dayCounter.isSame(today),
                        isFuture: dayCounter.isAfter(today),
                        isWeekend: [
                            0,
                            6
                        ].indexOf(dayCounter.day()) > -1
                    });
                    dayCounter.add(1, 'day');
                }
                var eventsSorted = filterEventsInPeriod(events, startOfWeek, endOfWeek).map(function (event) {
                    var eventStart = moment(event.startsAt).startOf('day');
                    var eventEnd = moment(event.endsAt).startOf('day');
                    var weekViewStart = moment(startOfWeek).startOf('day');
                    var weekViewEnd = moment(endOfWeek).startOf('day');
                    var offset, span;
                    if (eventStart.isBefore(weekViewStart) || eventStart.isSame(weekViewStart)) {
                        offset = 0;
                    } else {
                        offset = eventStart.diff(weekViewStart, 'days');
                    }
                    if (eventEnd.isAfter(weekViewEnd)) {
                        eventEnd = weekViewEnd;
                    }
                    if (eventStart.isBefore(weekViewStart)) {
                        eventStart = weekViewStart;
                    }
                    span = moment(eventEnd).diff(eventStart, 'days') + 1;
                    event.daySpan = span;
                    event.dayOffset = offset;
                    return event;
                });
                return {
                    days: days,
                    events: eventsSorted
                };
            }
            function getDayView(events, currentDay, dayStartHour, dayEndHour, hourHeight) {
                var calendarStart = moment(currentDay).startOf('day').add(dayStartHour, 'hours');
                var calendarEnd = moment(currentDay).startOf('day').add(dayEndHour, 'hours');
                var calendarHeight = (dayEndHour - dayStartHour + 1) * hourHeight;
                var hourHeightMultiplier = hourHeight / 60;
                var buckets = [];
                var eventsInPeriod = filterEventsInPeriod(events, moment(currentDay).startOf('day').toDate(), moment(currentDay).endOf('day').toDate());
                return eventsInPeriod.map(function (event) {
                    if (moment(event.startsAt).isBefore(calendarStart)) {
                        event.top = 0;
                    } else {
                        event.top = moment(event.startsAt).startOf('minute').diff(calendarStart.startOf('minute'), 'minutes') * hourHeightMultiplier - 2;
                    }
                    if (moment(event.endsAt).isAfter(calendarEnd)) {
                        event.height = calendarHeight - event.top;
                    } else {
                        var diffStart = event.startsAt;
                        if (moment(event.startsAt).isBefore(calendarStart)) {
                            diffStart = calendarStart.toDate();
                        }
                        event.height = moment(event.endsAt).diff(diffStart, 'minutes') * hourHeightMultiplier;
                    }
                    if (event.top - event.height > calendarHeight) {
                        event.height = 0;
                    }
                    event.left = 0;
                    return event;
                }).filter(function (event) {
                    return event.height > 0;
                }).map(function (event) {
                    var cannotFitInABucket = true;
                    buckets.forEach(function (bucket, bucketIndex) {
                        var canFitInThisBucket = true;
                        bucket.forEach(function (bucketItem) {
                            if (eventIsInPeriod(event.startsAt, event.endsAt, bucketItem.startsAt, bucketItem.endsAt) || eventIsInPeriod(bucketItem.startsAt, bucketItem.endsAt, event.startsAt, event.endsAt)) {
                                canFitInThisBucket = false;
                            }
                        });
                        if (canFitInThisBucket && cannotFitInABucket) {
                            cannotFitInABucket = false;
                            event.left = bucketIndex * 150;
                            buckets[bucketIndex].push(event);
                        }
                    });
                    if (cannotFitInABucket) {
                        event.left = buckets.length * 150;
                        buckets.push([event]);
                    }
                    return event;
                });
            }
            return {
                getWeekDayNames: getWeekDayNames,
                getYearView: getYearView,
                getMonthView: getMonthView,
                getWeekView: getWeekView,
                getDayView: getDayView
            };
        }
    ]);
    'use strict';
    angular.module('mwl.calendar').service('calendarDebounce', [
        '$timeout',
        function ($timeout) {
            function debounce(func, wait, immediate) {
                var timeout;
                return function () {
                    var context = this, args = arguments;
                    function later() {
                        timeout = null;
                        if (!immediate) {
                            func.apply(context, args);
                        }
                    }
                    var callNow = immediate && !timeout;
                    $timeout.cancel(timeout);
                    timeout = $timeout(later, wait);
                    if (callNow) {
                        func.apply(context, args);
                    }
                };
            }
            return debounce;
        }
    ]);
    'use strict';
    angular.module('mwl.calendar').provider('calendarConfig', function () {
        var defaultDateFormats = {
            hour: 'ha',
            day: 'D MMM',
            month: 'MMMM',
            weekDay: 'dddd'
        };
        var defaultTitleFormats = {
            day: 'dddd D MMMM, YYYY',
            week: 'Week {week} of {year}',
            month: 'MMMM YYYY',
            year: 'YYYY'
        };
        var i18nStrings = {
            eventsLabel: 'Events',
            timeLabel: 'Time'
        };
        var configProvider = this;
        configProvider.setDateFormats = function (formats) {
            angular.extend(defaultDateFormats, formats);
            return configProvider;
        };
        configProvider.setTitleFormats = function (formats) {
            angular.extend(defaultTitleFormats, formats);
            return configProvider;
        };
        configProvider.setI18nStrings = function (strings) {
            angular.extend(i18nStrings, strings);
            return configProvider;
        };
        configProvider.$get = function () {
            return {
                dateFormats: defaultDateFormats,
                titleFormats: defaultTitleFormats,
                i18nStrings: i18nStrings
            };
        };
    });
    'use strict';
    angular.module('mwl.calendar').filter('calendarTruncateEventTitle', function () {
        return function (string, length, boxHeight) {
            if (!string) {
                return '';
            }
            //Only truncate if if actually needs truncating
            if (string.length >= length && string.length / 20 > boxHeight / 30) {
                return string.substr(0, length) + '...';
            } else {
                return string;
            }
        };
    });
    'use strict';
    angular.module('mwl.calendar').filter('calendarLimitTo', [
        'limitToFilter',
        function (limitToFilter) {
            if (angular.version.minor >= 4) {
                //1.4+ supports the begin attribute
                return limitToFilter;
            }
            //Copied from the angular source. Only 1.4 has the begin functionality.
            return function (input, limit, begin) {
                if (Math.abs(Number(limit)) === Infinity) {
                    limit = Number(limit);
                } else {
                    limit = parseInt(limit);
                }
                if (isNaN(limit)) {
                    return input;
                }
                if (angular.isNumber(input)) {
                    input = input.toString();
                }
                if (!angular.isArray(input) && !angular.isString(input)) {
                    return input;
                }
                begin = !begin || isNaN(begin) ? 0 : parseInt(begin);
                begin = begin < 0 && begin >= -input.length ? input.length + begin : begin;
                if (limit >= 0) {
                    return input.slice(begin, begin + limit);
                } else if (begin === 0) {
                    return input.slice(limit, input.length);
                } else {
                    return input.slice(Math.max(0, begin + limit), begin);
                }
            };
        }
    ]);
    'use strict';
    angular.module('mwl.calendar').directive('mwlDateModifier', function () {
        return {
            restrict: 'A',
            controller: [
                '$element',
                '$attrs',
                '$scope',
                'moment',
                function ($element, $attrs, $scope, moment) {
                    function onClick() {
                        if (angular.isDefined($attrs.setToToday)) {
                            $scope.date = new Date();
                        } else if (angular.isDefined($attrs.increment)) {
                            $scope.date = moment($scope.date).add(1, $scope.increment).toDate();
                        } else if (angular.isDefined($attrs.decrement)) {
                            $scope.date = moment($scope.date).subtract(1, $scope.decrement).toDate();
                        }
                        $scope.$apply();
                    }
                    $element.bind('click', onClick);
                    $scope.$on('$destroy', function () {
                        $element.unbind('click', onClick);
                    });
                }
            ],
            scope: {
                date: '=',
                increment: '=',
                decrement: '='
            }
        };
    });
    'use strict';
    angular.module('mwl.calendar').directive('mwlCollapseFallback', [
        '$injector',
        function ($injector) {
            if ($injector.has('collapseDirective')) {
                return {};
            }
            return {
                restrict: 'A',
                controller: [
                    '$scope',
                    '$attrs',
                    '$element',
                    function ($scope, $attrs, $element) {
                        var unbindWatcher = $scope.$watch($attrs.mwlCollapseFallback, function (shouldCollapse) {
                            if (shouldCollapse) {
                                $element.addClass('ng-hide');
                            } else {
                                $element.removeClass('ng-hide');
                            }
                        });
                        var unbindDestroy = $scope.$on('$destroy', function () {
                            unbindDestroy();
                            unbindWatcher();
                        });
                    }
                ]
            };
        }
    ]);
    'use strict';
    angular.module('mwl.calendar').directive('mwlCalendarYear', function () {
        return {
            templateUrl: 'src/templates/calendarYearView.html',
            restrict: 'EA',
            require: '^mwlCalendar',
            scope: {
                events: '=',
                currentDay: '=',
                onEventClick: '=',
                onEditEventClick: '=',
                onDeleteEventClick: '=',
                editEventHtml: '=',
                deleteEventHtml: '=',
                autoOpen: '=',
                onTimespanClick: '='
            },
            controller: [
                '$scope',
                'moment',
                'calendarHelper',
                function ($scope, moment, calendarHelper) {
                    var vm = this;
                    var firstRun = true;
                    $scope.$on('calendar.refreshView', function () {
                        vm.view = calendarHelper.getYearView($scope.events, $scope.currentDay);
                        //Auto open the calendar to the current day if set
                        if ($scope.autoOpen && firstRun) {
                            firstRun = false;
                            vm.view.forEach(function (month) {
                                if (moment($scope.currentDay).startOf('month').isSame(month.date)) {
                                    vm.monthClicked(month, true);
                                }
                            });
                        }
                    });
                    vm.monthClicked = function (month, monthClickedFirstRun) {
                        if (!monthClickedFirstRun) {
                            $scope.onTimespanClick({ calendarDate: month.date.toDate() });
                        }
                        vm.openRowIndex = null;
                        var monthIndex = vm.view.indexOf(month);
                        if (monthIndex === vm.openMonthIndex) {
                            //the month has been clicked and is already open
                            vm.openMonthIndex = null;    //close the open month
                        } else {
                            vm.openMonthIndex = monthIndex;
                            vm.openRowIndex = Math.floor(monthIndex / 4);
                        }
                    };
                }
            ],
            controllerAs: 'vm',
            link: function (scope, element, attrs, calendarCtrl) {
                scope.vm.calendarCtrl = calendarCtrl;
            }
        };
    });
    'use strict';
    angular.module('mwl.calendar').directive('mwlCalendarWeek', function () {
        return {
            templateUrl: 'src/templates/calendarWeekView.html',
            restrict: 'EA',
            require: '^mwlCalendar',
            scope: {
                events: '=',
                currentDay: '=',
                onEventClick: '='
            },
            controller: [
                '$scope',
                'calendarHelper',
                function ($scope, calendarHelper) {
                    var vm = this;
                    $scope.$on('calendar.refreshView', function () {
                        vm.view = calendarHelper.getWeekView($scope.events, $scope.currentDay);
                    });
                }
            ],
            controllerAs: 'vm',
            link: function (scope, element, attrs, calendarCtrl) {
                scope.vm.calendarCtrl = calendarCtrl;
            }
        };
    });
    'use strict';
    angular.module('mwl.calendar').directive('mwlCalendarSlideBox', function () {
        return {
            restrict: 'EA',
            templateUrl: 'src/templates/calendarSlideBox.html',
            replace: true,
            controller: [
                '$scope',
                '$sce',
                function ($scope, $sce) {
                    var vm = this;
                    vm.$sce = $sce;
                    var unbindWatcher = $scope.$watch('isOpen', function (isOpen) {
                        vm.shouldCollapse = !isOpen;
                    });
                    var unbindDestroy = $scope.$on('$destroy', function () {
                        unbindDestroy();
                        unbindWatcher();
                    });
                }
            ],
            controllerAs: 'vm',
            require: [
                '^?mwlCalendarMonth',
                '^?mwlCalendarYear'
            ],
            link: function (scope, elm, attrs, ctrls) {
                scope.isMonthView = !!ctrls[0];
                scope.isYearView = !!ctrls[1];
            },
            scope: {
                isOpen: '=',
                events: '=',
                onEventClick: '=',
                editEventHtml: '=',
                onEditEventClick: '=',
                deleteEventHtml: '=',
                onDeleteEventClick: '='
            }
        };
    });
    'use strict';
    angular.module('mwl.calendar').directive('mwlCalendarMonth', function () {
        return {
            templateUrl: 'src/templates/calendarMonthView.html',
            restrict: 'EA',
            require: '^mwlCalendar',
            scope: {
                events: '=',
                currentDay: '=',
                onEventClick: '=',
                onEditEventClick: '=',
                onDeleteEventClick: '=',
                editEventHtml: '=',
                deleteEventHtml: '=',
                autoOpen: '=',
                onTimespanClick: '='
            },
            controller: [
                '$scope',
                'moment',
                'calendarHelper',
                function ($scope, moment, calendarHelper) {
                    var vm = this;
                    var firstRun = true;
                    $scope.$on('calendar.refreshView', function () {
                        vm.weekDays = calendarHelper.getWeekDayNames();
                        vm.view = calendarHelper.getMonthView($scope.events, $scope.currentDay);
                        var rows = Math.floor(vm.view.length / 7);
                        vm.monthOffsets = [];
                        for (var i = 0; i < rows; i++) {
                            vm.monthOffsets.push(i * 7);
                        }
                        //Auto open the calendar to the current day if set
                        if ($scope.autoOpen && firstRun) {
                            firstRun = false;
                            vm.view.forEach(function (day) {
                                if (day.inMonth && moment($scope.currentDay).startOf('day').isSame(day.date)) {
                                    vm.dayClicked(day, true);
                                }
                            });
                        }
                    });
                    vm.dayClicked = function (day, dayClickedFirstRun) {
                        if (!dayClickedFirstRun) {
                            $scope.onTimespanClick({ calendarDate: day.date.toDate() });
                        }
                        vm.openRowIndex = null;
                        var dayIndex = vm.view.indexOf(day);
                        if (dayIndex === vm.openDayIndex) {
                            //the day has been clicked and is already open
                            vm.openDayIndex = null;    //close the open day
                        } else {
                            vm.openDayIndex = dayIndex;
                            vm.openRowIndex = Math.floor(dayIndex / 7);
                        }
                    };
                    vm.highlightEvent = function (event, shouldAddClass) {
                        vm.view.forEach(function (day) {
                            delete day.highlightClass;
                            if (shouldAddClass) {
                                var dayContainsEvent = day.events.indexOf(event) > -1;
                                if (dayContainsEvent) {
                                    day.highlightClass = 'day-highlight dh-event-' + event.type;
                                }
                            }
                        });
                    };
                }
            ],
            controllerAs: 'vm',
            link: function (scope, element, attrs, calendarCtrl) {
                scope.vm.calendarCtrl = calendarCtrl;
            }
        };
    });
    'use strict';
    angular.module('mwl.calendar').directive('mwlCalendarDay', function () {
        return {
            templateUrl: 'src/templates/calendarDayView.html',
            restrict: 'EA',
            require: '^mwlCalendar',
            scope: {
                events: '=',
                currentDay: '=',
                onEventClick: '=',
                dayViewStart: '=',
                dayViewEnd: '=',
                dayViewSplit: '='
            },
            controller: [
                '$scope',
                '$timeout',
                'moment',
                'calendarHelper',
                'calendarConfig',
                function ($scope, $timeout, moment, calendarHelper, calendarConfig) {
                    var vm = this;
                    var dayViewStart, dayViewEnd;
                    vm.calendarConfig = calendarConfig;
                    function updateDays() {
                        dayViewStart = moment($scope.dayViewStart || '00:00', 'HH:mm');
                        dayViewEnd = moment($scope.dayViewEnd || '23:00', 'HH:mm');
                        vm.dayViewSplit = parseInt($scope.dayViewSplit);
                        vm.hourHeight = 60 / $scope.dayViewSplit * 30;
                        vm.hours = [];
                        var dayCounter = moment(dayViewStart);
                        for (var i = 0; i <= dayViewEnd.diff(dayViewStart, 'hours'); i++) {
                            vm.hours.push({ label: dayCounter.format(calendarConfig.dateFormats.hour) });
                            dayCounter.add(1, 'hour');
                        }
                    }
                    var originalLocale = moment.locale();
                    $scope.$on('calendar.refreshView', function () {
                        if (originalLocale !== moment.locale()) {
                            originalLocale = moment.locale();
                            updateDays();
                        }
                        vm.view = calendarHelper.getDayView($scope.events, $scope.currentDay, dayViewStart.hours(), dayViewEnd.hours(), vm.hourHeight);
                    });
                    updateDays();
                }
            ],
            controllerAs: 'vm'
        };
    });
    'use strict';
    angular.module('mwl.calendar').directive('mwlCalendar', function () {
        return {
            templateUrl: 'src/templates/calendar.html',
            restrict: 'EA',
            scope: {
                events: '=',
                view: '=',
                viewTitle: '=',
                currentDay: '=',
                editEventHtml: '=',
                deleteEventHtml: '=',
                autoOpen: '=',
                onEventClick: '&',
                onEditEventClick: '&',
                onDeleteEventClick: '&',
                onTimespanClick: '&',
                onDrillDownClick: '&',
                dayViewStart: '@',
                dayViewEnd: '@',
                dayViewSplit: '@'
            },
            controller: [
                '$scope',
                '$timeout',
                'moment',
                'calendarTitle',
                'calendarDebounce',
                function ($scope, $timeout, moment, calendarTitle, calendarDebounce) {
                    var vm = this;
                    $scope.events = $scope.events || [];
                    vm.changeView = function (view, newDay) {
                        $scope.view = view;
                        $scope.currentDay = newDay;
                    };
                    vm.drillDown = function (date) {
                        var rawDate = moment(date).toDate();
                        var nextView = {
                            year: 'month',
                            month: 'day',
                            week: 'day'
                        };
                        if ($scope.onDrillDownClick({
                                calendarDate: rawDate,
                                calendarNextView: nextView[$scope.view]
                            }) !== false) {
                            vm.changeView(nextView[$scope.view], rawDate);
                        }
                    };
                    var previousDate = moment($scope.currentDay);
                    var previousView = angular.copy($scope.view);
                    //Use a debounce to prevent it being called 3 times on initialisation
                    var refreshCalendar = calendarDebounce(function () {
                        if (calendarTitle[$scope.view]) {
                            $scope.viewTitle = calendarTitle[$scope.view]($scope.currentDay);
                        }
                        //if on-timespan-click="calendarDay = calendarDate" is set then dont update the view as nothing needs to change
                        var currentDate = moment($scope.currentDay);
                        var shouldUpdate = true;
                        if (previousDate.clone().startOf($scope.view).isSame(currentDate.clone().startOf($scope.view)) && !previousDate.isSame(currentDate) && $scope.view === previousView) {
                            shouldUpdate = false;
                        }
                        previousDate = currentDate;
                        previousView = angular.copy($scope.view);
                        if (shouldUpdate) {
                            $scope.$broadcast('calendar.refreshView');
                        }
                    }, 50);
                    //Auto update the calendar when the locale changes
                    var unbindLocaleWatcher = $scope.$watch(function () {
                        return moment.locale();
                    }, refreshCalendar);
                    var unbindOnDestroy = [];
                    unbindOnDestroy.push(unbindLocaleWatcher);
                    //Refresh the calendar when any of these variables change.
                    unbindOnDestroy.push($scope.$watch('currentDay', refreshCalendar));
                    unbindOnDestroy.push($scope.$watch('view', refreshCalendar));
                    unbindOnDestroy.push($scope.$watch('events', refreshCalendar, true));
                    //Remove any watchers when the calendar is destroyed
                    var unbindDestroyListener = $scope.$on('$destroy', function () {
                        unbindOnDestroy.forEach(function (unbind) {
                            unbind();
                        });
                    });
                    unbindOnDestroy.push(unbindDestroyListener);
                }
            ]
        };
    });
}(window, angular));