                //切换背景
                $("input").click(function(){
					var val= $(this).attr("value");
					var tit = $(this).attr("tit");
					 var code_val = $(".code_control").attr("value");
					 
					 if(""!=val){
						
						   switch (tit){
						    case "username":
							    $("#username").css("background","url(images/loginWindow_inputPic01.png) no-repeat");
								 if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
								break;
						    case "pw": 
							    $("#pw").css("background","url(images/loginWindow_inputPic01.png) no-repeat");
								 if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
							    break;                           
						    case "domain": 
							    $("#domain").css("background","url(images/loginWindow_inputPic01.png) no-repeat");
								 if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
							    break; 
						    };  
						 
					  }else{
						 
						  switch (tit){
							  
						    case "username": 
							   $("#username").css("background","url(images/login_username02.png) no-repeat");
							    if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
							   break;
						    case "pw":
							   $("#pw").css("background","url(images/login_pw02.png) no-repeat"); 
							    if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
							    break;
						   case "domain": 
						         $("#domain").css("background","url(images/login_domain02.png) no-repeat");
							    if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
							     break; 
							case "code":
							  if(""==code_val){ $(".code_control").css("background","url(images/login_code.png) no-repeat");
								}else{$(".code_control").css("background","url(images/login_code04.png) no-repeat");}
							    $("#code").css("background","url(images/login_code03.png) no-repeat");  break; 
						  }
						}
					 $(this).select(); 
				 });
				 //失焦事件
				 function tblur(obj){
				  	 var val = $(obj).attr("value");
					 var tit = $(obj).attr("tit");
					 if(""==val){
						  switch (tit){
						    case "username":
							    $("#username").css("background","url(images/login_username.png) no-repeat");break;
						    case "pw": $("#pw").css("background","url(images/login_pw.png) no-repeat");  break;                            case "domain": $("#domain").css("background","url(images/login_domain.png) no-repeat");break; 
							case "code": $("#code").css("background","url(images/login_code.png) no-repeat");  break; 
						  };  
					}else{
						 switch (tit){
						    case "username":
							    $("#username").css("background","url(images/loginWindow_inputPic02.png) no-repeat");
								break;
						    case "pw": 
							    $("#pw").css("background","url(images/loginWindow_inputPic02.png) no-repeat");
							    break;                           
						    case "domain": 
							    $("#domain").css("background","url(images/loginWindow_inputPic02.png) no-repeat");
							    break; 
							case "code": $("#code").css("background","url(images/login_code04.png) no-repeat"); 
							    break; 
						  };  
					    
				    }	 
				 };
				 // 键盘事件
				  $("input").keydown(function(event){
                     var tit = $(this).attr("tit");
					  switch (tit){
						case "username":
					    $("#username").css("background","url(images/loginWindow_inputPic01.png) no-repeat"); break;
						case "pw":
						$("#pw").css("background","url(images/loginWindow_inputPic01.png) no-repeat"); break;  
						case "domain":
						$("#domain").css("background","url(images/loginWindow_inputPic01.png) no-repeat"); break;
						case "code":
						$("#code").css("background","url(images/login_code02.png) no-repeat"); break;	
						  }	 
                   });
				   
				   //提交按钮事件
				   $(".subm input").mousedown(function(){
					  $(".subm").css("background","url(images/loginBtn03.png) no-repeat"); 
					 
				   });
				   $(".subm input").mouseup(function(){
					  $(".subm").css("background","url(images/loginBtn02.png) no-repeat");  
				   });
				   
		/* code _control*/		
         $(".code_control").click(function(){
			 var val= $(this).attr("value");
			 if(""==val){
				 $(this).css("background","url(images/login_code03.png) no-repeat");
			 }else{
				  $(this).css("background","url(images/login_code02.png) no-repeat");
			 }
		 });
		 $(".code_control").keydown(function(event){
			  $(this).css("background","url(images/login_code02.png) no-repeat");
		});
		
		$(document).ready(function(){
		  var username_val = $("#username input").attr("value");
		  var pw_val = $("#pw input").attr("value");
		  var domain_val = $("#domain input").attr("value");
		  var code_cVal = $(".code_control").attr("value");
		  
		  if(""!=username_val){$("#username").css("background","url(images/loginWindow_inputPic02.png) no-repeat"); }
		  if(""!=pw_val){$("#pw").css("background","url(images/loginWindow_inputPic02.png) no-repeat"); }
		  if(""!=domain_val){$("#domain").css("background","url(images/loginWindow_inputPic02.png) no-repeat"); }
		  if(""!=code_cVal){$(".code_control").css("background","url(images/login_code04.png) no-repeat"); }
		  
		  
		    $(".long").dblclick(function(){
			  $(this).css("background","url(images/loginWindow_inputPic01.png) no-repeat");
			  $(this).select(); 
		  });
		   $(".code_control").dblclick(function(){
			  $(".code_control").css("background","url(images/login_code02.png) no-repeat");
			  $(this).select(); 
		  });
		});