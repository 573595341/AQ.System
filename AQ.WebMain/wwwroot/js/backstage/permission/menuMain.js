
Vue.component('component-menu', {
    template: '#my-component-menu',
    props: ['menu', 'menuList'],
    data: function () {
        return {
            /*子菜单样式*/
            childClass: {
                'resource-child': true,
                'resource-child-hide': false
            },
            /*三角图标样式*/
            triangleClass: {
                'triangle-right': false,
                'triangle-bottom': true
            },
            /**/
            operation: {
                view: {
                    key: 'view',
                    value: 1
                },
                add: {
                    key: 'add',
                    value: 2
                },
                edit: {
                    key: 'edit',
                    value: 4
                },
                delete: {
                    key: 'delete',
                    value: 8
                }
            },
            dataSource: []
        };
    },
    watch: {},
    methods: {

        /*展开收缩子菜单*/
        toggleChild: function () {
            var _this = this;
            if (_this.dataSource.length > 0) {
                _this.childClass['resource-child-hide'] = !_this.childClass['resource-child-hide'];
                _this.triangleClass['triangle-bottom'] = !_this.childClass['resource-child-hide'];
                _this.triangleClass['triangle-right'] = _this.childClass['resource-child-hide'];
            }
        },
        /*获取子菜单集合*/
        getSubmenu: function (menuId) {
            var _this = this;
            if (menuId == undefined) { return []; }
            let r = [];
            for (let index = 0; index < _this.menuList.length; index++) {
                const element = _this.menuList[index];
                if (element.ParentId == menuId) {
                    r.push(element);
                }
            }
            _this.dataSource = r;
            return _this.dataSource;
        },
        /*菜单操作*/
        menuClick: function (value, e) {
            var _this = this;
            _this.setAllCheck(value, e.target.checked);
            _this.refreshModule(value, e.target.checked);
            // console.log(type + ' == ' + _this.operation[type].allChecked);
        },
        /*子菜单操作*/
        submenuClick: function (value, index, e) {
            var _this = this;
            if (e.target.checked) {
                if ((_this.dataSource[index].Value & value) != value) {
                    _this.dataSource[index].Value = _this.dataSource[index].Value | value;
                }
                if ((_this.menu.Value & value) != value) {
                    _this.menu.Value = _this.menu.Value | value;
                }
            } else {
                if ((_this.dataSource[index].Value & value) == value) {
                    _this.dataSource[index].Value = _this.dataSource[index].Value ^ value;
                }
                _this.refreshMenu(value);
            }
            _this.refreshModule(value, e.target.checked);
            return;
        },
        /*根据子菜单刷新全选*/
        refreshMenu: function (value) {
            var _this = this;
            let isAll = false;
            for (let i = 0; i < _this.dataSource.length; i++) {
                const item = _this.dataSource[i];
                if ((item.Value & value) == value) {
                    isAll = true;
                    break;
                }
            }
            if (isAll) {
                if ((_this.menu.Value & value) != value) {
                    _this.menu.Value = _this.menu.Value | value;
                }
            } else {
                if (!isAll && (_this.menu.Value & value) == value) {
                    _this.menu.Value = _this.menu.Value ^ value;
                }
            }
        },
        /*设置全选/取消全选*/
        setAllCheck: function (value, isChecked) {
            var _this = this;
            if (isChecked) {
                if ((_this.menu.Value & value) != value) {
                    _this.menu.Value = _this.menu.Value | value;
                }
                _this.dataSource.forEach(m => {
                    if ((m.Value & value) != value) {
                        m.Value = m.Value | value;
                    }
                });
            } else {
                if ((_this.menu.Value & value) == value) {
                    _this.menu.Value = _this.menu.Value ^ value;
                }
                _this.dataSource.forEach(m => {
                    if ((m.Value & value) == value) {
                        m.Value = m.Value ^ value;
                    }
                });
            }
        },
        /*刷新模块*/
        refreshModule: function (value, isChecked) {
            this.$emit('callback', value, isChecked);
        }
    },
    created: function () {
        var _this = this;
        _this.dataSource = _this.getSubmenu(_this.menu.Id);
    }
});
layui.use(['layer'], function () {
    var layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl;

    var app = new Vue({
        el: '#app',
        data: {
            roleId: getQuery('roleId'),
            moduleClass: {
                'resource-module-select': false
            },
            isRouterAlive: true,
            //enabledModule: true,
            currentIndex: 0,
            operation: {
                view: {
                    key: 'view',
                    value: 1,
                },
                add: {
                    key: 'add',
                    value: 2,
                },
                edit: {
                    key: 'edit',
                    value: 4,
                },
                delete: {
                    key: 'delete',
                    value: 8,
                }
            },
            menuData: [
                //{
                //    Id: '1001',
                //    Name: '一级菜单1',
                //    ParentId: '',
                //    Value: 0
                //}, {
                //    Id: '1002',
                //    Name: '一级菜单2',
                //    ParentId: '',
                //    Value: 0
                //}, {
                //    Id: '2001',
                //    Name: '二级菜单1-1',
                //    ParentId: '1001',
                //    Value: 0
                //}, {
                //    Id: '2002',
                //    Name: '二级菜单1-2',
                //    ParentId: '1001',
                //    Value: 0
                //}
                //, {
                //    Id: '2003',
                //    Name: '二级菜单2-1',
                //    ParentId: '1002',
                //    Value: 0
                //}, {
                //    Id: '2004',
                //    Name: '二级菜单2-2',
                //    ParentId: '1002',
                //    Value: 0
                //},
            ],
            moduleData: [
                //{
                //    Id: 'm001',
                //    Name: '模块一',
                //    Value: 0
                //}, {
                //    Id: 'm002',
                //    Name: '模块二',
                //    Value: 0
                //}, {
                //    Id: 'm003',
                //    Name: '模块三',
                //    Value: 0
                //}, {
                //    Id: 'm004',
                //    Name: '模块四',
                //    Value: 0
                //}
            ],
            isFirstWatch: true,
            isModify: false,
        },
        watch: {
            menuData: {
                deep: true,
                handler: function (newValue, oldValue) {
                    var _this = this;
                    if (_this.isFirstWatch) {
                        _this.isFirstWatch = false;
                        return;
                    }
                    _this.isModify = true;
                }
            },
        },
        methods: {
            reload() {
                this.isRouterAlive = false
                this.$nextTick(() => (this.isRouterAlive = true))
            },
            getMenuList: function () {
                var _this = this;
                var r = [];
                for (let index = 0; index < _this.menuData.length; index++) {
                    const element = _this.menuData[index];
                    if (element.ParentId == '0' || element.ParentId == '') {
                        r.push(element);
                    }
                }
                return r;
            },
            moduleClick: function (index) {
                var _this = this;
                if (_this.currentIndex == index) { return; }
                if (_this.isModify) { //信息修改提示
                    layer.confirm('保存当前修改?', { icon: 3, title: '提示' }, function (i) { //确认
                        if (_this.saveData() === false) {
                            layer.close(i);
                            return;
                        }
                        _this.toModule(index);
                        layer.close(i);
                    }, function (i) { //取消
                        _this.toModule(index);
                        layer.close(i);
                    });
                } else {
                    _this.toModule(index);
                }
            },
            toModule: function (index) {
                var _this = this;
                if (index < 0 || index > _this.moduleData.length - 1) { index = 0; }
                _this.currentIndex = index;
                _this.getMenuData(_this.moduleData[index].Id, _this.roleId);
                _this.reload();
                for (var key in _this.operation) {
                    _this.refreshAllChecked(_this.operation[key].value, false);
                }
                _this.resetFirstWatch();
            },
            allClick: function (value, e) {
                var _this = this;
                if (e.target.checked) {
                    if ((_this.moduleData[_this.currentIndex].Value & value) != value) {
                        _this.moduleData[_this.currentIndex].Value = _this.moduleData[_this.currentIndex].Value | value;
                    }
                } else {
                    if ((_this.moduleData[_this.currentIndex].Value & value) == value) {
                        _this.moduleData[_this.currentIndex].Value = _this.moduleData[_this.currentIndex].Value ^ value;
                    }
                }
                for (let i = 0; i < _this.$refs.menu.length; i++) {
                    const element = _this.$refs.menu[i];
                    element.setAllCheck(value, e.target.checked);
                }

            },
            refreshAllChecked: function (value, isChecked) {
                var _this = this;
                if (isChecked) {
                    if ((_this.moduleData[_this.currentIndex].Value & value) != value) {
                        _this.moduleData[_this.currentIndex].Value = _this.moduleData[_this.currentIndex].Value | value;
                    }
                } else {
                    //console.log('value:' + value);
                    //_this.menuData.forEach(d => {
                    //    console.log(JSON.stringify(d));
                    //});
                    var isAll = false;
                    for (let i = 0; i < _this.menuData.length; i++) {
                        const menu = _this.menuData[i];
                        if ((menu.Value & value) == value) {
                            isAll = true;
                            break;
                        }
                    }
                    if (isAll) {
                        if ((_this.moduleData[_this.currentIndex].Value & value) != value) {
                            _this.moduleData[_this.currentIndex].Value = _this.moduleData[_this.currentIndex].Value | value;
                        }
                    } else {
                        if ((_this.moduleData[_this.currentIndex].Value & value) == value) {
                            _this.moduleData[_this.currentIndex].Value = _this.moduleData[_this.currentIndex].Value ^ value;
                        }
                    }
                }
            },
            saveData: function () {
                var _this = this;
                //_this.menuData.forEach(d => {
                //    console.log(JSON.stringify(d));
                //});
                var result = false;
                $.ajax({
                    type: 'post',
                    url: '/Admin/Permission/SaveMenu',
                    data: { roleId: _this.roleId, module: _this.moduleData[_this.currentIndex], menu: _this.menuData },
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        if (r.ResultCode == 0) {
                            result = true;
                            _this.resetFirstWatch();
                            layer.alert('操作成功', { icon: 6 });
                        } else {
                            layer.alert(r.ResultMsg, { icon: 5 });
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        layer.alert('操作失败', { icon: 5 });
                    }
                });
                return result;
            },
            getModuleData: function () {
                var _this = this;
                $.ajax({
                    type: 'post',
                    url: '/Admin/Permission/GetModuleData',
                    data: { roleId: _this.roleId },
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        //debugger;
                        if (r.ResultCode == 0) {
                            _this.moduleData = r.Data;
                        } else {
                            layer.alert(r.ResultMsg, { icon: 5 });
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        layer.alert('操作失败', { icon: 5 });
                    }
                });
            },
            getMenuData: function (mId, rId) {
                var _this = this;
                $.ajax({
                    type: 'post',
                    url: '/Admin/Permission/GetMenuData',
                    data: { moduleId: mId, roleId: rId },
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        //debugger;
                        if (r.ResultCode == 0) {
                            _this.menuData = r.Data;
                        } else {
                            layer.alert(r.ResultMsg, { icon: 5 });
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        layer.alert('操作失败', { icon: 5 });
                    }
                });
            },
            resetFirstWatch: function () {
                this.isFirstWatch = true;
                this.isModify = false;
            },
        },
        created: function () {
            var _this = this;
            _this.getModuleData();
            _this.getMenuData(_this.moduleData[0].Id, _this.roleId);
        }
    });

    //window.onload = function () {
    //    debugger;
    //    app.$watch('menuData', function () {
    //        console.log('发生变化了');
    //    }, { deep: true });  
    //}
})
