
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
            _this.childClass['resource-child-hide'] = !_this.childClass['resource-child-hide'];
            _this.triangleClass['triangle-bottom'] = !_this.childClass['resource-child-hide'];
            _this.triangleClass['triangle-right'] = _this.childClass['resource-child-hide'];
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
        _this.dataSource = _this.getSubmenu(_this.menu.MenuId);
    }
});
layui.use([], function () {
    var layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl;

    var app = new Vue({
        el: '#app',
        data: {
            isRouterAlive: true,
            menuData: [
                {
                    MenuId: '1001',
                    MenuName: '一级菜单1',
                    ParentId: '',
                    Value: 0
                }, {
                    MenuId: '1002',
                    MenuName: '一级菜单2',
                    ParentId: '',
                    Value: 0
                }, {
                    MenuId: '2001',
                    MenuName: '二级菜单1-1',
                    ParentId: '1001',
                    Value: 0
                }, {
                    MenuId: '2002',
                    MenuName: '二级菜单1-2',
                    ParentId: '1001',
                    Value: 0
                }
                , {
                    MenuId: '2003',
                    MenuName: '二级菜单2-1',
                    ParentId: '1002',
                    Value: 0
                }, {
                    MenuId: '2004',
                    MenuName: '二级菜单2-2',
                    ParentId: '1002',
                    Value: 0
                },
            ],
            moduleData: [
                {
                    ModuleId: 'm001',
                    ModuleName: '模块一',
                    Value: 0

                }, {
                    ModuleId: 'm002',
                    ModuleName: '模块二',
                    Value: 0
                }, {
                    ModuleId: 'm003',
                    ModuleName: '模块三',
                    Value: 0
                }, {
                    ModuleId: 'm004',
                    ModuleName: '模块四',
                    Value: 0
                }
            ],
            enabledModule: true,
            currentIndex: 0,
            moduleClass: {
                'resource-module-select': false
            },
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
        },
        watch: {
            enabledModule: function (newValue, oldValue) {
                console.log(this.currentIndex + ' = ' + newValue + ' ' + oldValue)
            }
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
                    if (element.ParentId == '') {
                        r.push(element);
                    }
                }
                return r;
            },
            enabledModuleClick: function () {
                this.enabledModule = !this.enabledModule;
            },
            moduleClick: function (index) {
                var _this = this;
                if (index < 0 || index > _this.moduleData.length - 1) { index = 0; }
                _this.currentIndex = index;
                _this.reload();
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
                    let isAll = false;
                    for (let i = 0; i < _this.menuData.length; i++) {
                        const menu = _this.menuData[i];
                        if ((menu.Value & value) == value) {
                            isAll = true;
                            break;
                        }
                    }
                    if (!isAll && (_this.moduleData[_this.currentIndex].Value & value) == value) {
                        _this.moduleData[_this.currentIndex].Value = _this.moduleData[_this.currentIndex].Value ^ value;
                    }
                }
            },
            save: function () {
                var _this = this;
                _this.menuData.forEach(d => {
                    console.log(JSON.stringify(d));
                });
                alert(1);
            },
            getData: function () {
                $.ajax({
                    url:'/Admin/'
                });
            }
        },
        created: function () {
            // var _this = this;
        }
    });

})
