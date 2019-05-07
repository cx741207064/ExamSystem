layui.use(['jquery', 'common', 'layer', 'element'], function() {
	var $ = layui.jquery,
		layer = layui.layer,
		common = layui.common,
		element = layui.element;

	$('#Org_Name').html(Params.getCookie('E3CEEA3AC665C2E56FDD3DE4DABE4F6B'));

	common.larryCmsLoadJq('../common/plus/jquery.leoweather.min.js', function() {
		$('#weather').leoweather({
			format: '，{时段}好！<span id="colock">现在时间是：<strong>{年}年{月}月{日}日 星期{周} {时}:{分}:{秒}</strong>'
		});
		$('#closeInfo').on('click', function() {
			$('#infoSwitch').hide();
		});
	});
});