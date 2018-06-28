angular.module('home.station', ['app.common'])
    .value("myValue",
    {
        "distinctIndex": 0,
        "stationGrade": "",
        "indoorGrade": "",
        "netType": "",
        "indoorNetType": "",
        "roomAttribution": "",
        "towerAttribution": "",
        "isPower": "",
        "isBBU": "",
        "isNew": "",
        "indoortype": "",
        "coverage": ""
    })
    .controller("menu.assessment",
    function ($scope,
        downSwitchService,
        myValue,
        baiduMapService,
        geometryService,
        parametersDialogService,
        collegeMapService,
        dumpPreciseService,
        appUrlService,
        generalMapService) {
        $scope.assessment = function () {
            parametersDialogService.showAssessmentListDialog();
        };
        $scope.assessment();

    })
   


