var slider_img;
var images = [];
var index = 0;

function setImg() {
    return slider_img.setAttribute("src", images[index]);
}

$(function () {
    if (window.location.pathname.match('/home') || (window.location.pathname.match('/') && window.location.pathname.length == 1)) {
        var slidesControl = document.querySelectorAll('.slide');
        var btnsControl = document.querySelectorAll('.btn-slide');
        let currentSlide = 1;

        // script will control nagivation slider
        var manualNav = function (manual) {
            slidesControl.forEach((slide) => {
                slide.classList.remove('active');
            });
            btnsControl.forEach((button) => {
                button.classList.remove('active');
            });

            slidesControl[manual].classList.add('active');
            btnsControl[manual].classList.add('active');
        }

        btnsControl.forEach((button, i) => {
            button.addEventListener('click', () => {
                manualNav(i);
                currentSlide = i;
            });
        });

        var slideRotate = function (activeClass) {
            let active = document.getElementsByClassName('active');
            let index = 1;

            var rotator = () => {
                setTimeout(function () {
                    [...active].forEach((activeClass) => {
                        activeClass.classList.remove('active');
                    });

                    slidesControl[index].classList.add('active');
                    btnsControl[index].classList.add('active');
                    index++;

                    if (slidesControl.length == index) {
                        index = 0;
                    }

                    if (index >= slidesControl.length) {
                        return;
                    }
                    rotator();
                }, 5000);
            }
            rotator();
        }
        slideRotate();
    }

    if (window.location.pathname.match('/galeria')) {
        $(".arrow-nav").on("click", function () {
            slider_img = document.querySelector(".modal-image");

            if (images.length == 0) {
                $(".box-image-flat img").each(function () {
                    images.push($(this).attr("src"));
                });
            }

            if ($(this).hasClass("arrow-prev")) {
                if (index <= 0) {
                    index = images.length;
                }
                index--;
                return setImg();
            }

            if ($(this).hasClass("arrow-next")) {
                if (index >= images.length - 1) {
                    index = -1;
                }
                index++;
                return setImg();
            }
        });
    }
});