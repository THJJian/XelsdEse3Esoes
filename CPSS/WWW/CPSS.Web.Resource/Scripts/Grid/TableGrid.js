(
    function(ns) {
        CSPPLib.RegNameSpace(ns).TableGrid = (function () {
            var self;
            
            var _init = function (_grid_control_id_, _grid_show_footer_, _columns_, _init_field_, _init_grid_row_count_, _button_panel_id_, _head_panel_id_, _grid_panel_id_) {
                self = this;
                self.columns = _columns_;
                self.init_grid_row_count = 5;
                self.init_grid_row_data = [];
                if (typeof _init_grid_row_count_ === "number")
                    self.init_grid_row_count = _init_grid_row_count_;

                self.grid_control_id = ["#", _grid_control_id_].join("");
                self.button_panel_id = ["#", typeof _button_panel_id_ === "undefined" ? "billbutton" : _button_panel_id_].join("");
                self.head_panel_id = ["#", typeof _head_panel_id_ === "undefined" ? "billhead" : _head_panel_id_].join("");
                self.grid_panel_id = ["#", typeof _grid_panel_id_ === "undefined" ? "billgrid" : _grid_panel_id_].join("");
                if (self.columns.length === 0) throw "表格列初始化错误。";

                //初始化表格行数
                for (var _row_count = 0; _row_count < self.init_grid_row_count; _row_count++) {
                    var __data = {};
                    for (var _cell_count = 0; _cell_count < self.columns.length; _cell_count++) {
                        var __item = self.columns[_cell_count];
                        __data[__item.field] = "";
                    }
                    self.init_grid_row_data.push(__data);
                }

                self.grid = $(self.grid_control_id)
                    .datagrid({
                        columns: [
                            self.columns
                        ],
                        data: {
                            rows: self.init_grid_row_data
                        },
                        rownumbers: true,
                        showFooter: _grid_show_footer_
                    })
                    .datagrid("enableCellEditing")
                    .datagrid("gotoCell",
                    {
                        index: 0,
                        field: _init_field_
                    });

                ///region 设置表格的最大高度 start
                var _window_height = $(window).height(),
                    _button_panel = $(self.button_panel_id).panel("panel"),
                    _button_height = $(_button_panel).height(),
                    _button_padding_top = parseInt($(_button_panel).css("padding-top")),
                    _button_padding_bottom = parseInt($(_button_panel).css("padding-bottom")),
                    _button_border_bottom_width = parseInt($(_button_panel).css("border-bottom-width")),
                    _button_border_top_width = parseInt($(_button_panel).css("border-top-width")),
                    _head_panel = $(self.head_panel_id).panel("panel"),
                    _head_height = $(_head_panel).height(),
                    _head_padding_top = parseInt($(_head_panel).css("padding-top")),
                    _head_padding_bottom = parseInt($(_head_panel).css("padding-bottom")),
                    _head_border_bottom_width = parseInt($(_head_panel).css("border-bottom-width")),
                    _head_border_top_width = parseInt($(_head_panel).css("border-top-width")),
                    _grid_panel = $(self.grid_panel_id).panel("panel"),
                    _grid_padding_top = parseInt($(_grid_panel).css("padding-top")),
                    _grid_padding_bottom = parseInt($(_grid_panel).css("padding-bottom")),
                    _grid_border_bottom_width = parseInt($(_grid_panel).css("border-bottom-width")),
                    _grid_border_top_width = parseInt($(_grid_panel).css("border-top-width"));

                $(self.grid_panel_id).panel({
                    height: (_window_height - _grid_padding_top - _grid_padding_bottom - _grid_border_bottom_width - _grid_border_top_width - _head_height - _head_padding_top - _head_padding_bottom - _head_border_bottom_width - _head_border_top_width - _button_height - _button_padding_top - _button_padding_bottom - _button_border_bottom_width - _button_border_top_width)
                });

                $($(self.grid_panel_id).find("div.datagrid-wrap")).css({ "border-left": 0 });

                //region 设置表格的最大高度 end

                return self;
            };

            var _insertEmptyRow = function (_insertedIndex_) {
                if (_insertedIndex_ < self.init_grid_row_count) {
                    self.grid.datagrid("deleteRow", _insertedIndex_);
                }

                self.grid.datagrid("insertRow",
                {
                    index: _insertedIndex_,
                    row: self.init_grid_row_data[0]
                });
                return self;
            };

            return {
                init: _init,
                insertEmptyRow: _insertEmptyRow
            };
        })();
    }
)("UI");