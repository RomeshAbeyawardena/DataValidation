const del = require("delete");

const dest = "./Nuget/*.nupkg";

del([dest],
  function(err, deleted) {
    if (err) throw err;
    console.log(deleted);
  });