﻿layui.use(['table', 'form'], function () {
    var layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        form = layui.form,
        table = layui.table;

    var objData = {
        tableIns: null,
        Delete: function (id) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: 'Delete',
                data: { keys: [id] },
                dataType: "json",
                headers: {
                    "PAGETOKEN": $("input[name='PageToken']").val()
                },
                success: function (r) {
                    if (r.ResultCode == 0) {
                        layer.alert('操作成功', { icon: 6 }, function (index) {
                            _this.tableIns = table.reload(_this.tableIns.config.id,
                                {
                                    page: {
                                        curr: (_this.tableIns.config.page.curr > 1 && table.cache[_this.tableIns.config.id].length == 1)
                                            ? _this.tableIns.config.page.curr - 1 : _this.tableIns.config.page.curr
                                    }
                                });
                            layer.close(index);
                        });
                    } else {
                        alert(r.ResultMsg, { icon: 5 });
                    }
                },
                error: function () {
                    alert('请求错误, 请稍后再试', { icon: 5 });
                }
            })
        }
        , ChangeStatus: function (ids, value) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: 'ChangeStatus',
                data: { keys: ids, status: value },
                dataType: "json",
                headers: {
                    "PAGETOKEN": $("input[name='PageToken']").val()
                },
                success: function (r) {
                    if (r.ResultCode == 0) {
                        _this.tableIns = table.reload(_this.tableIns.config.id);
                    } else {
                        alert(r.ResultMsg, { icon: 5 });
                        _this.tableIns = table.reload(_this.tableIns.config.id);
                    }
                },
                error: function () {
                    alert('请求错误, 请稍后再试', { icon: 5 });
                    _this.tableIns = table.reload(_this.tableIns.config.id);
                }
            });
        }
    }

    //分页列表
    objData.tableIns = table.render({
        elem: '#dataList',
        url: 'DataBind',
        method: 'post',
        //cellMinWidth: 60,
        page: true,
        //height: "full-125",
        limits: [10, 15, 20, 25],
        toolbar: '#toolbar',
        defaultToolbar: ['filter', 'exports'],
        loading: true,
        even: true,
        request: {
            pageName: 'PageIndex',
            limitName: 'PageSize'
        },
        where: {
            //search: 'adasd'
        },
        //done: function (res, curr, count) {
        //    //console.log('curr==' + curr);
        //},
        response: {
            statusName: 'ResultCode'    //规定数据状态的字段名称，默认：code
            , statusCode: 0             //规定成功的状态码，默认：0
            , msgName: 'ResultMsg'      //规定状态信息的字段名称，默认：msg
            , countName: 'TotalData'    //规定数据总数的字段名称，默认：count
            , dataName: 'Data'          //规定数据列表的字段名称，默认：data
        },
        cols: [[
            { type: 'checkbox', fixed: "left" }
            , { field: 'Id', title: 'ID', align: "center" }
            , { field: 'Account', title: '账号', align: "center" }
            , { field: 'CName', title: '姓名', align: "center" }
            , { field: 'NickName', title: '昵称', align: "center" }
            , { field: 'Mobile', title: '手机号码', align: "center" }
            , { field: 'JobCode', title: '工号', align: "center" }
            , {
                field: 'Sex', title: '性别', align: "center", templet: function (d) {
                    return d.Sex > 0 ? d.Sex == 1 ? '男' : '女' : '暂无';
                }
            }
            , {
                field: 'Birthday', title: '生日', align: 'center', templet: function (d) {
                    return d.Birthday ? d.Birthday.substring(0, 10) : '';
                }
            }
            , { field: 'Status', title: '状态', align: 'center', templet: '#status' }
            , { fixed: 'right', title: '操作', minWidth: 200,toolbar: '#operation' }
        ]]
    });

    //行工具事件
    table.on('tool(dataList)', function (obj) {
        switch (obj.event) {
            case 'edit':
                location.href = 'Details?id=' + obj.data.Id;
                break;
            case 'delete':
                layer.confirm('确认删除该数据?', { icon: 3, title: '提示' }, function (index) {
                    //obj.del();
                    objData.Delete(obj.data.Id);
                    layer.close(index);
                });
                break;
            case 'role':
                //alert(1);
                window.parent.toTab({ url: '/Admin/Role/Index', text: '角色1', refresh: true });
                break;
            default:
                break;
        }
    });

    //工具栏事件
    table.on('toolbar(dataList)', function (obj) {
        debugger;
        switch (obj.event) {
            case 'add':
                location.href = 'Details';
                break;
            case 'refresh':
                objData.tableIns = table.reload(objData.tableIns.config.id);
                break;

            default:
                break;
        }
    });

    //表单事件
    form.on('switch(changeStatus)', function (obj) {
        objData.ChangeStatus([$(obj.elem).data('id')], 1 - obj.value);
    })

})