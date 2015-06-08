(function () {
    'use strict';

    angular.module('inventory').factory("dataServiceList", ["$http", "$q", function ($http, $q) {

        var _item = {};
        var _itmes = [];

        var _isInit = false;
        var _isListReady = function () {
            return _isInit;
        }

        var _isItemInit = false;
        var _isItemReady = function () {
            return _isItemInit;
        }

        var _listHeader = [
        {
            field: 'Title',
            title: 'PRODUCTS',
            reverse: undefined,
            cssClass: 'list-grid-column-80'
        },
        {
            field: 'Price',
            title: 'PRICE',
            cssClass: 'text-right list-grid-column-20'
        }];

        var _searchInfo = {
            page: 1,
            itemsPerPage: 15,
            searchProduct: '',
            sort: []
        };

        var _getList = function () {
            _isInit = false;
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/Inventory',
                params: { parameters: _searchInfo }
            }).then(function (result) {
                // Successful
                angular.copy(result.data, _itmes);
                _isInit = true;
                deferred.resolve(result);
            },
              function (rejection) {
                  // Error
                  //cop in bunk object
                  angular.copy({ totalItemCount: 0, items: [] }, _itmes);
                  _isInit = true;
                  //update satus
                  deferred.reject(rejection);
              });
            return deferred.promise;
        };

        var _getItem = function (item) {
            _isItemInit = false;
            var deferred = $q.defer();

            $http({
                method: 'GET',
                url: '/api/Inventory?title=' + item
            }).then(function (result) {
                // Successful
                angular.copy(result.data, _item);
                _isItemInit = true;
                deferred.resolve(result);
            },
              function (rejection) {
                  // Error
                  //cop in bunk object
                  angular.copy({}, _item);
                  _isItemInit = true;
                  //update satus
                  deferred.reject(rejection);
              });
            return deferred.promise;
        };

        var _clearItem = function () {
            _isItemInit = false;
            
        };

        return {
            isListReady: _isListReady,
            isItemReady: _isItemReady,
            getList: _getList,
            getItem: _getItem,
            clearItem: _clearItem,
            list: _itmes,
            item: _item,
            headerColumn: _listHeader,
            searchInfo: _searchInfo
        };
    }]);

})();