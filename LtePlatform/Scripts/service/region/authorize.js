angular.module('region.authorize', ['app.core'])
    .constant('roleDistrictDictionary', [
        {
            role: "顺德管理",
            district: "顺德"
        }, {
            role: "南海管理",
            district: "南海"
        }, {
            role: "禅城管理",
            district: "禅城"
        }, {
            role: "三水管理",
            district: "三水"
        }, {
            role: "高明管理",
            district: "高明"
        }
    ])
    .factory('authorizeService', function(generalHttpService, roleDistrictDictionary) {
        return {
            queryCurrentUserInfo: function() {
                return generalHttpService.getApiData('CurrentUser', {});
            },
            queryCurrentUserName: function() {
                return generalHttpService.getApiDataWithHeading('CurrentUserName', {});
            },
            queryAllUsers: function() {
                return generalHttpService.getApiDataWithHeading('ApplicationUsers', {});
            },
            queryRolesInUser: function(userName) {
                return generalHttpService.getApiDataWithHeading('ApplicationUsers', {
                    userName: userName
                });
            },
            queryCandidateRolesInUser: function(userName) {
                return generalHttpService.getApiDataWithHeading('ManageUsers', {
                    userName: userName
                });
            },
            queryEmailConfirmed: function(name) {
                return generalHttpService.getMvcData('/Manage/EmailHasBeenConfirmed', {
                    userName: name
                });
            },
            updateRoleList: function() {
                return generalHttpService.getApiDataWithHeading('ApplicationRoles', {});
            },
            addRole: function(name) {
                return generalHttpService.getApiDataWithHeading('CreateRole', {
                    roleName: name
                });
            },
            deleteRole: function(name) {
                return generalHttpService.getApiDataWithHeading('DeleteRole', {
                    roleName: name
                });
            },
            assignRoleInUser: function(userName, roleName) {
                return generalHttpService.getApiDataWithHeading('ApplicationRoles', {
                    userName: userName,
                    roleName: roleName
                });
            },
            releaseRoleInUser: function(userName, roleName) {
                return generalHttpService.getApiDataWithHeading('ManageRoles', {
                    userName: userName,
                    roleName: roleName
                });
            },
            changePassword: function(input) {
                return generalHttpService.postMvcData('/Manage/ChangePassword', input);
            },
            forgotPassword: function(input) {
                return generalHttpService.postMvcData('/Manage/ForgotPassword', input);
            },
            resetPassword: function(input) {
                return generalHttpService.postMvcData('/Manage/ResetPassword', input);
            },
            addPhoneNumber: function(input) {
                return generalHttpService.postMvcData('/Manage/AddPhoneNumber', input);
            },
            verifyPhoneNumber: function(input) {
                return generalHttpService.postMvcData('/Manage/VerifyPhoneNumber', input);
            },
            removePhoneNumber: function() {
                return generalHttpService.postMvcData('/Manage/RemovePhoneNumber', {});
            },
            confirmEmail: function(input) {
                return generalHttpService.postMvcData('/Manage/ConfirmEmail', input);
            },
            queryRoleDistricts: function (roles) {
                var districts = [];
                angular.forEach(roleDistrictDictionary, function(dict) {
                    var role = _.find(roles, function(x) { return x === dict.role });
                    if (role) {
                        districts.push(dict.district);
                    }
                });
                return districts;
            }
        };
    });