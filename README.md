# Yumicr #
**简介**：使用unity2019开发的2D横版游戏的demo<br/>
**使用的unity版本**：2019.3.9f<br/>
**可运行平台**：<del>大概只有</del>Windows
<br/>
## 已完成的游戏内容 ##
+ ### 主角 ###
	+ 基本移动与攻击
	+ 跳跃、下落、奔跑和攻击的动画切换
+ ### 怪物 ###
	+ **青蛙**<br/>在一定范围内来回跳跃
	+ **恶魔**<br/>在一定范围内来回巡逻
	+ **鹰**<br/>当玩家未进入特定范围时，鹰上下来回巡逻，当玩家进入特定范围时使用A星算法追踪玩家位置直至玩家离开一定范围
	+ **幽灵**<br/>无实体的怪物，不会与墙体发生碰撞。游荡一小段时间后会扑向此时玩家角色所在位置，只会由boss召唤产生
+ ### BOSS ###
	+ **<del>憨批</del>AI** 
	<br/>游荡一段时间后随即选取攻击方式：近距离或远距离，选择远距离攻击时从三中咒术中选取一个，近距离攻击会快速移动（此时boss会有残影）到玩家身边进行吐息，由于没钱买行为树插件，因此使用animation的event来实现行动循环
	+ **攻击方式**
		+ 雷电球
		<br/> boss在身边召唤出五个雷电球向玩家目前所处位置进行攻击，雷电球释放后不会改变运动方向
		+ 召唤幽灵
		<br/> 召唤两个低生命值的幽灵
		+ 火焰骷髅头
		<br/> boss上下移动直到与玩家持同一水平线，然后向前方释放一个会不断前进的火焰骷髅头

+ ### NPC ###
	能够对话，没了。	
+ ### 场景 ###
	+ 开始场景
	+ 一个完全的普通关卡
	+ 一个没怪物的普通关卡
	+ 一个没做完的boss房间
## 安装package ##
+ 使用cinemachine插件来取代了原生的摄像机
+ 使用了A* Pathfinding项目来实现怪物鹰的追踪功能，[A星项目地址](https://arongranberg.com/astar/)

## TODO  ##
+ 加上<del>做嗨了忘了加的</del>血条
+ 给怪物恶魔加上射线检测，当发现玩家角色时转换为警戒状态，移动速度增加并追踪玩家直至玩家离开一定范围
+ 技能树系统（让NPC有点用）
+ 任务系统（让NPC有点用）
+ 调整bossAI，目前boss攻击循环过快

## ！注意！ ##
由于本人不熟悉C#，同时仅为unity初学者，因此可能发生如下情况：


+ 把C#当C艹使，比如使用C艹的命名风格，本项目[命名规范参考](https://zh-google-styleguide.readthedocs.io/en/latest/google-cpp-styleguide/naming/)
+ 同样的事务可能出现多种可互相替代的实现方式，比如定时调用可能同时出现以下三种方式：
	+ 使用Invoke
	+ 使用无敌的协程
	+ 维护一个time_count，每次update减去Time.deltatime，当time_count为0时调用方法并重置time_count的值
