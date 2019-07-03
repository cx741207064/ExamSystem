layui.config({
	base: '/common/lib/'
});
layui.use(['jquery','layer','element','common','larryMenu','form'],function(){
	var $ = layui.$,
	layer = layui.layer,
	common = layui.common,
	device = layui.device(),
	form = layui.form,
	element = layui.element;
    // 页面上下文菜单
    larryMenu = layui.larryMenu();
    // 右键菜单控制
    var larrycmsMenuData = [
	];
	// larryMenu.ContentMenu(larrycmsMenuData,{
    //      name: "html" 
	// },$('html'));
	// 右键菜单结束
	$('#larry_tab_content', parent.document).mouseout(function(){
         larryMenu.remove();
	});
});