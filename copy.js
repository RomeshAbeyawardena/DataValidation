const copyfiles = require("copyfiles");

const source = "./**/release/*.nupkg";
const dest = "./Nuget";
const options = {
  up: 3,
  version:true,
  soft: false,
  flat: true
};

copyfiles([source, dest], options,
  function() {

  });