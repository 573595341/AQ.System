layui.use(['element', 'form'], function () {
    var layer = parent.layer === undefined ? layui.layer : top.layer
        , $ = layui.jquery
        , form = layui.form
        , isModify = $.trim($('#id').val()) ? true : false;

    var objData = {
        Init: function () {
            this.GetParentMenu();
            this.GetModule();
            if (isModify) {
                this.GetData();
            }
        }
        , LoadSelectOption: function (elem, data, valueField, textField) {
            if (Object.prototype.toString.call(data) != '[object Array]') {
                return;
            }
            var html = '';
            $.each(data, function (i, v) {
                html += '<option value="' + v[valueField] + '">' + v[textField] + '</option>';
            });
            $(elem).append(html);
            form.render("select");
        }
        , GetParentMenu: function () {
            var _this = this;
            $.ajax({
                type: 'post',
                url: 'MenuData',
                dataType: "json",
                headers: {
                    "PAGETOKEN": $("input[name='PageToken']").val()
                },
                async: false,
                success: function (r) {
                    if (r.ResultCode == 0) {
                        _this.LoadSelectOption('#ParentId', r.Data, 'Id', 'Name');
                    } else {
                        layer.alert(r.ResultMsg, { icon: 5 });
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    layer.alert('操作失败', { icon: 5 });
                }
            });
        }
        , GetModule: function () {
            var _this = this;
            $.ajax({
                type: 'post',
                url: 'ModuleData',
                dataType: "json",
                headers: {
                    "PAGETOKEN": $("input[name='PageToken']").val()
                },
                async: false,
                success: function (r) {
                    if (r.ResultCode == 0) {
                        _this.LoadSelectOption('#ModuleId', r.Data, 'Id', 'Name');
                    } else {
                        layer.alert(r.ResultMsg, { icon: 5 });
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    layer.alert('操作失败', { icon: 5 });
                }
            });
        }
        , SaveData: function (paras) {
            if (!paras) {
                layer.alert('操作失败', { icon: 5 });
                return;
            }
            var _this = this;
            var reqUrl = isModify ? 'Edit' : 'Add';
            $.ajax({
                type: 'post',
                url: reqUrl,
                data: paras,
                dataType: "json",
                headers: {
                    "PAGETOKEN": $("input[name='PageToken']").val()
                },
                success: function (r) {
                    if (r.ResultCode == 0) {
                        layer.alert('操作成功', { icon: 6 }, function (index) {
                            history.back(-1);
                            layer.close(index);
                        });
                    } else {
                        layer.alert(r.ResultMsg, { icon: 5 });
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    layer.alert('操作失败', { icon: 5 });
                }
            });
        }
        , GetParas: function (field) {
            if (!field) { return null; }
            return {
                Id: $('#id').val()
                , Name: field.Name
                , ParentId: field.ParentId
                , ModuleId: field.ModuleId
                , PageUrl: field.PageUrl
                , Sort: field.Sort
                , Ico: field.Ico
                , Status: field.Status == "1" ? 1 : 0
            };
        }
        , GetData: function () {
            var _this = this;
            var mid = $.trim($('#id').val());
            if (!mid) { return; }
            $.ajax({
                type: 'post',
                url: 'GetInfo',
                data: { id: mid },
                dataType: "json",
                headers: {
                    "PAGETOKEN": $("input[name='PageToken']").val()
                },
                success: function (r) {
                    if (r.ResultCode == 0) {
                        _this.DataBind(r.Data);
                    } else {
                        layer.alert(r.ResultMsg, { icon: 5 });
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    layer.alert('操作失败', { icon: 5 });
                }
            });
        }
        , DataBind: function (resultData) {
            if (!resultData) { return; }
            form.val("formData", {
                Name: resultData.Name
                , ParentId: resultData.ParentId
                , ModuleId: resultData.ModuleId
                , PageUrl: resultData.PageUrl
                , Sort: resultData.Sort
                , Ico: resultData.Ico
                , Status: resultData.Status
            });
        }
    }

    objData.Init();

    //信息验证
    form.verify({
        name: function (value, item) {
            if (value.length < 10) {
                return '不能少于10个字符'
            }
        }
    });

    //保存
    form.on('submit(save)', function (data) {
        //console.log(data.elem) //被执行事件的元素DOM对象，一般为button对象
        //console.log(data.form) //被执行提交的form对象，一般在存在form标签时才会返回
        //console.log(data.field) //当前容器的全部表单字段，名值对形式：{name: value}
        $(data.elem).attr("disabled", "disabled").addClass('layui-btn-disabled');
        setTimeout(function () {
            $(data.elem).removeAttr("disabled", "disabled").removeClass('layui-btn-disabled');
        }, 3000);
        var paras = objData.GetParas(data.field);
        objData.SaveData(paras);
        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    });

})