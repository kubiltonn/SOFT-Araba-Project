// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
  const modellerMenu = document.getElementById("modellerMenu");
  const modellerDropdown = document.getElementById("modellerDropdown");
  if (modellerDropdown) {
    modellerDropdown.style.display = "none";
  }
  if (modellerMenu && modellerDropdown) {
    modellerMenu.addEventListener("click", function (e) {
      e.preventDefault();
      modellerDropdown.style.display =
        modellerDropdown.style.display === "flex" ? "none" : "flex";
    });
    document.addEventListener("click", function (e) {
      if (
        !modellerMenu.contains(e.target) &&
        !modellerDropdown.contains(e.target)
      ) {
        modellerDropdown.style.display = "none";
      }
    });
  }
});

// Modern arayüz için ek JS
$(document).ready(function () {
  // Bilgi butonuna tıklanınca modalı aç
  $("#infoBtn").on("click", function () {
    var modal = new bootstrap.Modal(document.getElementById("infoModal"));
    modal.show();
  });

  // Sayfa yüklendiğinde toast göster
  var toastEl = document.getElementById("liveToast");
  if (toastEl) {
    var toast = new bootstrap.Toast(toastEl);
    toast.show();
  }

  // Modern video oynatıcı overlay
  $(".modern-video-wrapper").each(function () {
    var wrapper = $(this);
    var video = wrapper.find("video")[0];
    var overlay = wrapper.find(".modern-video-overlay");
    var playBtn = wrapper.find(".modern-play-btn");
    // Overlayi sadece video duruyorsa göster
    function updateOverlay() {
      if (video.paused) {
        overlay.fadeIn(200);
      } else {
        overlay.fadeOut(100);
      }
    }
    // Hover ile overlay göster
    wrapper.on("mouseenter", updateOverlay);
    wrapper.on("mouseleave", function () {
      overlay.fadeOut(100);
    });
    // Butona tıklayınca video başlasın
    playBtn.on("click", function (e) {
      e.preventDefault();
      video.play();
      overlay.fadeOut(100);
    });
    // Video durunca overlay tekrar gelsin
    $(video).on("pause", updateOverlay);
    // Sayfa yüklenince overlay duruma göre gösterilsin
    updateOverlay();
  });

  // Modern mega menu
  var $dropdown = $(".mega-dropdown");
  var $toggle = $(".mega-toggle");
  var $menu = $(".mega-menu");

  $toggle.on("click", function (e) {
    e.preventDefault();
    var $parent = $(this).closest(".mega-dropdown");
    $parent.toggleClass("open");
    // Diğer açık menüleri kapat
    $(".mega-dropdown").not($parent).removeClass("open");
  });

  // Dışarı tıklayınca kapansın
  $(document).on("click", function (e) {
    if (!$(e.target).closest(".mega-dropdown").length) {
      $(".mega-dropdown").removeClass("open");
    }
  });

  // Hover ile açılma (sadece masaüstü için)
  if (window.innerWidth > 700) {
    $dropdown.hover(
      function () {
        $(this).addClass("open");
      },
      function () {
        $(this).removeClass("open");
      }
    );
  }
});

// Parallax hero arka plan efekti
function softParallaxHero() {
  var heroBg = document.querySelector(".audi-hero-bg");
  if (!heroBg) return;
  var scrollY = window.scrollY || window.pageYOffset;
  // Parallax oranı: daha küçük değer daha yavaş hareket
  var parallax = scrollY * 0.25;
  heroBg.style.transform = "translateY(" + parallax + "px) scale(1.08)";
}
window.addEventListener("scroll", softParallaxHero);
window.addEventListener("DOMContentLoaded", softParallaxHero);

// MICRO-INTERACTIONS

// 2. Menüde Aktif Link Altı Animasyonu
$(function () {
  var path = window.location.pathname.toLowerCase();
  $(".audi-main-nav .nav-link").each(function () {
    var href = $(this).attr("href");
    if (href && path.indexOf(href.toLowerCase()) !== -1) {
      $(this).addClass("active");
    } else {
      $(this).removeClass("active");
    }
  });
});

// 3. Toast Fade Animasyonu (Bootstrap toast zaten fade destekliyor, ama ekstra garanti için)
$(function () {
  $(".toast").on("show.bs.toast", function () {
    $(this).addClass("showing");
  });
  $(".toast").on("shown.bs.toast", function () {
    $(this).removeClass("showing").addClass("show");
  });
  $(".toast").on("hide.bs.toast", function () {
    $(this).removeClass("show").addClass("showing");
  });
  $(".toast").on("hidden.bs.toast", function () {
    $(this).removeClass("showing");
  });
});

// TOOLTIP & POPOVER INIT
$(function () {
  // Tooltip
  $("[data-bs-toggle='tooltip']").tooltip({
    trigger: "hover focus",
    animation: true,
    delay: { show: 200, hide: 80 },
    placement: "top",
    container: "body",
    boundary: "window",
  });
  // Popover
  $("[data-bs-toggle='popover']").popover({
    trigger: "hover focus",
    animation: true,
    delay: { show: 200, hide: 100 },
    placement: "top",
    container: "body",
    boundary: "window",
    html: true,
  });
});

// GLOBAL LOADER
$(window).on("load", function () {
  setTimeout(function () {
    $("#global-loader").addClass("hide");
  }, 200); // kısa bir gecikme ile fade
});

// PAGE FADE ANIMATION (SPA olmadan) - loader ile entegre
$(function () {
  var $main = $("#main-content");
  if ($main.length) {
    $main.addClass("fade-in");
    $(document).on(
      "click",
      'a:not([target]):not([href^="#"]):not([data-bs-toggle]):not(.dropdown-toggle):not(.mega-toggle)',
      function (e) {
        var href = $(this).attr("href");
        if (href && href.indexOf("javascript:") !== 0 && href !== "#") {
          e.preventDefault();
          $("#global-loader").removeClass("hide");
          $main.removeClass("fade-in").addClass("fade-out");
          setTimeout(function () {
            window.location = href;
          }, 400);
        }
      }
    );
  }
});
