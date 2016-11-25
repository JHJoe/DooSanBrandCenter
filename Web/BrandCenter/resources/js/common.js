//HTML5 태그 block 선언
document.createElement('header');
document.createElement('footer');
document.createElement('section');
document.createElement('aside');
document.createElement('nav');
document.createElement('article');

//select ui
(function($){
	$.fn.extend({
		customStyle : function(options) {
			if(!$.browser.msie || ($.browser.msie&&$.browser.version>6)){
				return this.each(function() {
					var currentSelected = $(this).find(':selected');
						$(this).after('<span class="customStyleSelectBox"><span class="customStyleSelectBoxInner">'+currentSelected.text()+'</span><img src="../resources/images/common/bg_blank.png" alt="" class="bg_blank" /></span>').css({position:'absolute', opacity:0,fontSize:$(this).next().css('font-size')});
					var selectBoxSpan = $(this).next();
					var selectBoxWidth = parseInt($(this).width()) - parseInt(selectBoxSpan.css('padding-left')) -parseInt(selectBoxSpan.css('padding-right'));
					var selectBoxSpanInner = selectBoxSpan.find(':first-child');
						selectBoxSpan.css({display:'inline-block'});
						selectBoxSpanInner.css({width:selectBoxWidth, display:'inline-block'});
					var selectBoxHeight = parseInt(selectBoxSpan.height()) + parseInt(selectBoxSpan.css('padding-top')) + parseInt(selectBoxSpan.css('padding-bottom'));
						$(this).height(selectBoxHeight).change(function(){
						selectBoxSpanInner.text($(this).find(':selected').text()).parent().addClass('changed');
					});
				});
			}
		}
	});
})(jQuery);

$(function(){
	$('.select_ui').customStyle();
});