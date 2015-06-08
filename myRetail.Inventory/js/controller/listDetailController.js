(function () {
    'use strict';

    angular.module('inventory').controller('listDetailController', [
        '$scope', 'dataServiceList', '$routeParams', '$location',
        function ($scope, dataServiceList, $routeParams, $location) {
            $scope.data = dataServiceList;
            $scope.dsiplayItem = false;

            //runs the search for items
            $scope.search = function () {
                //might want to indicate that search is going on.
                $scope.ErrorObject = null;
                $scope.HasError = false;
                $scope.data.getList()
                  .then(function (result) {
                      // success
                  },
                  function (rejection) {
                      $scope.HasError = true;
                      $scope.ErrorObject = rejection;
                  })
                  .then(function () {
                      //might remove load
                  });
            }

            //sort the items
            $scope.sort = function (header) {
                if (header.reverse === false) {
                    header.reverse = undefined;
                }
                else {
                    header.reverse = !header.reverse;
                }
                var sortBy = header.field;
                //loop hrough sort array
                var arrayLength = $scope.data.searchInfo.sort.length;
                var removeAt = -1;
                var addNew = true;
                for (var i = 0; i < arrayLength; i++) {
                    if ($scope.data.searchInfo.sort[i].field === sortBy) {
                        //if alread in and set to revers then remove entry
                        if ($scope.data.searchInfo.sort[i].reverse === true) {
                            removeAt = i;
                        } else {
                            //selec flip the diresction 
                            $scope.data.searchInfo.sort[i].reverse = true;
                        }
                        addNew = false;
                        break;
                    }
                }

                //check if we need to remove or add it
                if (removeAt > -1 && addNew === false) {
                    $scope.data.searchInfo.sort.splice(removeAt, 1);
                }
                else if (addNew) {
                    $scope.data.searchInfo.sort.push({ field: sortBy, reverse: false });
                }

                $scope.data.searchInfo.page = 1;
                $scope.search();
            };

            //gets the item info
            $scope.getItem = function (item) {
                //$location.path(item);
                $location.url($location.path());
                $location.hash(item);
                $scope.ErrorObject = null;
                $scope.HasError = false;
                $scope.dsiplayItem = true;
                $scope.data.getItem(item)
                    .then(function (result) {
                        // success
                        $scope.dsiplayItem = true;
                    },
                        function (rejection) {
                            $scope.dsiplayItem = false;
                            $scope.HasError = true;
                            $scope.ErrorObject = rejection;
                        })
                    .then(function () {
                        //clean up
                    });
            };

            $scope.quantityDecrease = function () {
                if ($scope.data.item.quantity > 1) {
                    $scope.data.item.quantity--;
                }
            };

            $scope.quantityPlus = function () {
                $scope.data.item.quantity++;
            };

            $scope.reorder = function () {
                alert('Reorder submitted.');
            };

            $scope.backToList = function () {
                $location.url($location.path());
                $scope.dsiplayItem = false;
                $scope.data.clearItem();
            };

            //ok so since all in one view and using hash tag when change re run stuff.
            $scope.$watch(function () {
                return location.hash;
            }, function (value) {
                // do stuff
                if ($location.hash() === '') {
                    $scope.backToList();
                } else {
                    $scope.getItem($location.hash());
                }
            });

            //privste constructor.
            var init = function () {
                //if we are on the request screen and no case number then set to new request
                $scope.search();
                if ($location.hash() !== '') {
                    $scope.getItem($location.hash());
                }
            };

            //call init.
            init();


        }]);

})();