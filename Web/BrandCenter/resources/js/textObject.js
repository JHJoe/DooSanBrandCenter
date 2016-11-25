function TextObject () {
	//
	var id;
	var label;
	var x;
	var y;
	var font_color;
	var font_size;
	var font_align;
	var default_text;
	
	var container;
	var layer;
	var ctx;
	var hitLayer;
	var hitCtx;
	var isSelected = false;
	
	var obj = this;
	var width = 0;
	var height = 0;

	this.init = function(container, canvas, context) {
		this.container = container;
		this.layer = canvas;
		this.ctx = context;
		this.hitLayer = container.hitCanvas;
		this.hitCtx = this.hitLayer.context;
	};
	this.remove = function() {
		this.setSelected(false);
		if (this.layer) {
			this.layer.clear();
			this.container.destroy();
		}
		//console.log('textObject removed');
	};
	this.render = function() {
		if (!this.ctx) {
			console.log('context not exist : ' + this.ctx);
			return;
		}
		this.ctx.save();
		this.ctx.clearRect(0,0,5000,5000);
		this.ctx.fillStyle = this.font_color;
		this.ctx.font = this.font_size + 'px "Dotum"';
		this.ctx.textBaseline = 'top';
		this.ctx.textAlign = ["left", "center", "right"][this.font_align];
		this.ctx.fillText(this.default_text, this.x, this.y);
		//this.fillTextWithSpacing(this.ctx, this.default_text, this.x, this.y, -1);
		this.width = this.ctx.measureText(this.default_text).width;
		this.height = this.font_size;
		//highlight
		if (isSelected) {
			this.ctx.setLineDash([5, 3]);
			this.ctx.lineWidth = 2;
			this.ctx.strokeStyle="#CCCCCC";
			this.ctx.strokeRect((this.x*1)+0.5, (this.y*1)+0.5, this.width, this.height * 1.5);
			this.ctx.strokeStyle="#000000";
			this.ctx.strokeRect((this.x*1), (this.y*1), this.width, this.height * 1.5);
			/*
			this.ctx.globalAlpha=0.2;
			this.ctx.fillStyle = "#FF0000";
			this.ctx.fillRect(this.x, this.y, this.width, this.height * 1.5);
			*/
		}
		this.ctx.restore();
		
		this.hitCtx.save();
		this.hitCtx.clearRect(0,0,5000,5000);
		this.hitCtx.fillStyle = this.hitLayer.getColorFromKey(this.id);
		this.hitCtx.fillRect(this.x, this.y, this.width, this.height * 1.5);
		this.hitCtx.restore();
	};
	this.fillTextWithSpacing = function(context, text, x, y, spacing)
	{
		wAll = context.measureText(text).width;
		do
		{
			//Remove the first character from the string
			char = text.substr(0, 1);
			text = text.substr(1);

			//Print the first character at position (X, Y) using fillText()
			context.fillText(char, x, y);

			//Measure wShorter, the width of the resulting shorter string using measureText().
			if (text == "")
			wShorter = 0;
			else
			wShorter = context.measureText(text).width;

			//Subtract the width of the shorter string from the width of the entire string, giving the kerned width of the character, wChar = wAll - wShorter
			wChar = wAll - wShorter;

			//Increment X by wChar + spacing
			x += wChar + spacing;

			//wAll = wShorter
			wAll = wShorter;

			//Repeat from step 3
		} while (text != "");
	};
	this.setContext = function(context) {
		this.ctx = context;
	};
	this.setSelected = function(bool) {
		isSelected = bool; 
		this.render();
	};
	
	function highlight(bl) {
		//highlight selected item
		if (bl) {
			ctx.setLineDash([5, 3]);
			ctx.beginPath();
			ctx.moveTo(0,100);
			ctx.lineTo(400, 100);
			ctx.stroke();
		}
	}
};