// var LibraryGLClear = {
//     glClear: function(mask){
//         if(mask == 0x00004000){
//             var v = GLctx.getParameter(GLctx.COLOR_WRITEMASK);

//             if(!v[0] && !v[1] && !v[2] && v[3]){
//                 return;
//             }
//         }
//         GLctx.clear(mask);
//     }
// };

// mergeInto(LibraryManager.library, LibraryGLClear);

var LibraryGLClear = {
    glClear: function (mask) {
      if (mask == 0x00004000 && GLctx.dontClearOnFrameStart) {
        var v = GLctx.getParameter(GLctx.COLOR_WRITEMASK);
        if (!v[0] && !v[1] && !v[2] && v[3])
          GLctx.dontClearOnFrameStart = false;
          return;
      }
      GLctx.clear(mask);
    }
  };
  mergeInto(LibraryManager.library, LibraryGLClear);