(function($) {
    var plugin_name = 'favorite';

    $.fn[plugin_name] = function(selector, options) {
        var self = this;

        var settings = $.extend({
            selector: selector,
            serviceUrlGet: null,
            serviceUrlPost: null,
            serviceUrlPostRemove: null,
            startGet: true,
            classFav: 'active',
            classNoFav: 'inactive',
            data_already: 'already',
            onNotAuthenticated: null,
            onNotAuthenticatedAttrAdd: 'data-login-required',
            onNotAuthenticatedResultValue: 'NotAuthenticated',
        }, options);

        /*## METHODS ## */
        self.debug = function(data) {
            var message = 'debug: ' + plugin_name + ': ' + JSON.stringify(data);
            console.log(message);
        }

        self.ajaxerror = function(data) {
            self.debug(data);
        }

        self.doOnAuthenticated = function(item, callback) {
            if (item.ResultValue == settings.onNotAuthenticatedResultValue) {
                if (settings.onNotAuthenticated)
                    eval(settings.onNotAuthenticated)(item);
            } else if (callback)
                callback(item);
        }

        self.getSelectorName = function() {
            var value = settings.selector.substring(6);
            return value.substring(0, value.length - 1);
        }

        self.doClassFav = function(item) {
            var like = item.Like == true || item.Like == 'true';
            var $target = $('[data-' + self.getSelectorName() + '="' + item.Id + '"]');

            $target.removeClass(settings.classFav)
                .removeClass(settings.classNoFav)
                .addClass(like ? settings.classFav : settings.classNoFav).text(item.Total)
                .css({ cursor: "pointer" });

            if (item.ResultValue == settings.onNotAuthenticatedResultValue) {
                $target.attr(settings.onNotAuthenticatedAttrAdd, true).css({ cursor: "default" });
            }
        }

        self.doGet = function() {
            var serviceUrl = settings.serviceUrlGet;
            var id_collection = [];

            $(settings.selector).each(function() {
                var $target = $(this);
                var id = $target.data(self.getSelectorName());

                var value_attr_already = self.getSelectorName() + '-' + settings.data_already;
                if (!$target.data(value_attr_already))
                    id_collection.push(id);

                $target.attr('data-' + value_attr_already, true);
            });

            if (id_collection.length == 0)
                return;

            $.ajax({
                dataType: "json",
                url: serviceUrl,
                data: { ids: id_collection },
                traditional: true,
                success: function(data) { $(data).each(function(i, item) { self.doClassFav(item); }); },
                error: self.ajaxerror
            });
        }

        self.onClick = function($target) {
            var serviceUrl = settings.serviceUrlPost;
            var id = $target.data(self.getSelectorName());

            if ($target.hasClass(settings.classFav))
                serviceUrl = settings.serviceUrlPostRemove || settings.serviceUrlPost;

            serviceUrl = serviceUrl + '?id=' + id;
            $.post(serviceUrl, { id: id },
                function(data) {
                    $(data).each(function(i, item) {
                        self.doOnAuthenticated(item, function() {
                            self.doClassFav(item);
                        });
                    });
                }, 'json').fail(self.ajaxerror);
        }

        /*## INIT ## */
        $(document).on('click', settings.selector, function() { self.onClick($(this)); });

        if (settings.startGet)
            self.doGet(settings);

        return self;
    };

})(jQuery);