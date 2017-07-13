(function( $ ) {
    var plugin_name ='favorite';

$.fn[plugin_name] = function(options) {
    var self = this;
    var settings = $.extend({
        selector: this.selector,
        serviceUrlGet: null,
        serviceUrlPost: null,
        serviceUrlPostRemove: null,
        startGet: true,
        classFav: 'active',
        classNoFav: 'inactive'
    }, options );
    
    /*## METHODS ## */
    self.debug = function(data){
        var message = 'debug: ' + plugin_name + ': ' + JSON.stringify(data);
        alert(message);
        console.log(message);
    }

    self.ajaxerror = function(data){
        self.debug(data);
    }

    self.getSelectorName = function(){
        var value = settings.selector.substring(6);
        return value.substring(0,value.length-1);   
    }

    self.doClassFav = function(item){
        var like = item.Like=='true';
        var $target = $('[data-'+this.getSelectorName() +'="'+item.Id+'"]');
    
        $target.removeClass(settings.classFav);
        $target.removeClass(settings.classNoFav);
        $target.addClass(like ? settings.classFav : settings.classNoFav).text(item.Total);
    }

    self.doGet = function(){
      var serviceUrl = settings.serviceUrlGet;
      var id_collection = [];

      $(settings.selector).each(function(){
        var $target = $(this);
        var id =  $target.data(self.getSelectorName());
        id_collection.push(id);
      });     
   
     $.getJSON(serviceUrl,{ids: id_collection})
            .done(function(data){$(data).each(function(i, item){ self.doClassFav(item); });})
            .error(self.ajaxerror);
    }

    self.onClick  = function($target) 
    {
        var serviceUrl = settings.serviceUrlPost;
        var id =  $target.data(self.getSelectorName());

        if($target.hasClass(settings.classFav))
            serviceUrl = settings.serviceUrlPostRemove;

    $.post(serviceUrl , {id: id},null, 'json')
        .done(function(data)
        {
            $(data).each(function(i, item){
                if(id==item.Id) self.doClassFav(item); 
            });
        }).error(self.ajaxerror);  
    }

    /*## INIT ## */
    $(document).on('click',this.selector,function(){self.onClick( $(this));});

    if(settings.startGet)
        self.doGet(settings);

   return self;
};

})( jQuery );