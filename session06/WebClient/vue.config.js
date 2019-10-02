//
// https://cli.vuejs.org/config/#devserver
//
const path = require("path");

module.exports = {
  outputDir: path.resolve(__dirname, "wwwroot"),
  assetsDir: "",
  devServer: {
    port: '5001'
  }
}