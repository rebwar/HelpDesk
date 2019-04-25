(function($) {
	'use strict';
	jQuery(document).on('ready', function(){
        // Menu JS
        jQuery('.navbar .navbar-nav li a, .main-banner-content .btn-primary').on('click', function(e){
            var anchor = jQuery(this);
            jQuery('html, body').stop().animate({
                scrollTop: jQuery(anchor.attr('href')).offset().top - 50
            }, 1500);
            e.preventDefault();
        });
        jQuery(document).on('click','.navbar-collapse.in', function(e) {
            if( jQuery(e.target).is('a') && jQuery(e.target).attr('class') != 'dropdown-toggle' ) {
                $(this).collapse('hide');
            }
        });
		jQuery('.navbar .navbar-nav li a').on('click', function(){
			jQuery('.navbar-collapse').collapse('hide');
		});
        
        // Header Sticky
        jQuery(window).on('scroll',function() {
            if (jQuery(this).scrollTop() > 170){  
                jQuery('.navbar').addClass("is-sticky");
            }
            else{
                jQuery('.navbar').removeClass("is-sticky");
            }
        });
        
        // Services Slides
        $('.services-slides').owlCarousel({
            loop: true,
			rtl:true,
            autoplay:true,
            nav: true,
            mouseDrag: true,
            autoplayHoverPause: true,
            responsiveClass: true,
            dots: false,
            navText: [
            "<i class='icofont-curved-double-left'></i>",
            "<i class='icofont-curved-double-right'></i>"
            ],
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1024:{
                    items:3,
                },
                1200:{
                    items:3,
                }
            }
        });
        
        // Counter
        $(".count").counterUp({
            delay: 20,
            time: 1500
        });
        
        // Tabs
        (function ($) {
            $('.tab ul.tabs').addClass('active').find('> li:eq(0)').addClass('current');
            $('.tab ul.tabs li a').on('click', function (g) {
                var tab = $(this).closest('.tab'), 
                index = $(this).closest('li').index();
                tab.find('ul.tabs > li').removeClass('current');
                $(this).closest('li').addClass('current');
                tab.find('.tab_content').find('div.tabs_item').not('div.tabs_item:eq(' + index + ')').slideUp();
                tab.find('.tab_content').find('div.tabs_item:eq(' + index + ')').slideDown();
                g.preventDefault();
            });
	    })(jQuery);
        
        // Team Slides
        $('.team-slides').owlCarousel({
            loop: true,
			rtl:true,
            autoplay:true,
            nav: true,
            mouseDrag: true,
            autoplayHoverPause: true,
            responsiveClass: true,
            dots: false,
            navText: [
            "<i class='icofont-curved-double-left'></i>",
            "<i class='icofont-curved-double-right'></i>"
            ],
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1024:{
                    items:3,
                },
                1200:{
                    items:4,
                }
            }
        });
		
		// Features Slides
        $('.features-slides').owlCarousel({
            loop: true,
			rtl:true,
            autoplay:true,
            nav: true,
            mouseDrag: true,
            autoplayHoverPause: true,
            responsiveClass: true,
            dots: false,
            navText: [
            "<i class='icofont-curved-double-left'></i>",
            "<i class='icofont-curved-double-right'></i>"
            ],
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1024:{
                    items:3,
                },
                1200:{
                    items:4,
                }
            }
        });
		
		// Testimonials Slides
        $('.testimonials-slides').owlCarousel({
            loop: true,
			rtl:true,
            autoplay:true,
            nav: true,
            mouseDrag: true,
            autoplayHoverPause: true,
            responsiveClass: true,
            dots: false,
            navText: [
            "<i class='icofont-curved-double-left'></i>",
            "<i class='icofont-curved-double-right'></i>"
            ],
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1024:{
                    items:2,
                },
                1200:{
                    items:3,
                }
            }
        });
        
        // FAQ Accordion
        $(function() {
            $('.accordion').find('.accordion-title').on('click', function(){
                // Adds Active Class
                $(this).toggleClass('active');
                // Expand or Collapse This Panel
                $(this).next().slideToggle('fast');
                // Hide The Other Panels
                $('.accordion-content').not($(this).next()).slideUp('fast');
                // Removes Active Class From Other Titles
                $('.accordion-title').not($(this)).removeClass('active');		
            });
        });
        
        // Partner Slides
        $('.partner-slides').owlCarousel({
            loop: true,
			rtl:true,
            autoplay:true,
            nav: false,
            mouseDrag: true,
            autoplayHoverPause: true,
            responsiveClass: true,
            dots: false,
            navText: [
            "<i class='icofont-curved-double-left'></i>",
            "<i class='icofont-curved-double-right'></i>"
            ],
            responsive:{
                0:{
                    items:2,
                },
                768:{
                    items:4,
                },
                1200:{
                    items:6,
                }
            }
        });
		
		// Popup Youtube
		$('.popup-youtube').magnificPopup({
			disableOn: 320,
			type: 'iframe',
			mainClass: 'mfp-fade',
			removalDelay: 160,
			preloader: false,
			fixedContentPos: false
		});
        
        // Go to Top
        $(function(){
            //Scroll event
            $(window).on('scroll', function(){
                var scrolled = $(window).scrollTop();
                if (scrolled > 300) $('.go-top').fadeIn('slow');
                if (scrolled < 300) $('.go-top').fadeOut('slow');
            });  
            //Click event
            $('.go-top').on('click', function() {
                $("html, body").animate({ scrollTop: "0" },  500);
            });
        });
    });
    
    // Preloader JS
    jQuery(window).on('load', function() {
        $('.preloader').fadeOut();
    });
})(jQuery);