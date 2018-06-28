angular.module('app.menu', ['ui.grid', "ui.bootstrap"])
    .factory('menuItemService', function($uibModal, $log) {
        return {
            updateMenuItem: function(items, index, title, url, masterName) {
                if (index >= items.length) return;
                masterName = masterName || "";
                var subItems = items[index].subItems;
                for (var i = 0; i < subItems.length; i++) {
                    if (subItems[i].displayName === title) return;
                }
                subItems.push({
                    displayName: title,
                    url: url,
                    masterName: masterName
                });
                angular.forEach(items, function(item) {
                    item.isActive = false;
                });
                items[index].isActive = true;
            },
            showGeneralDialog: function(settings) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: settings.templateUrl,
                    controller: settings.controller,
                    size: settings.size || 'lg',
                    resolve: settings.resolve
                });
                modalInstance.result.then(function(info) {
                    console.log(info);
                }, function() {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showGeneralDialogWithAction: function(settings, action) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: settings.templateUrl,
                    controller: settings.controller,
                    size: settings.size || 'lg',
                    resolve: settings.resolve
                });
                modalInstance.result.then(function(info) {
                    action(info);
                }, function() {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showGeneralDialogWithDoubleAction: function(settings, action, action2) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: settings.templateUrl,
                    controller: settings.controller,
                    size: settings.size || 'lg',
                    resolve: settings.resolve
                });
                modalInstance.result.then(function(info) {
                    action(info);
                }, function() {
                    action2();
                });
            }
        };
    });