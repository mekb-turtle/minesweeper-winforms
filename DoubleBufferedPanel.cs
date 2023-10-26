using System.ComponentModel;
using System.Windows.Forms;

namespace Minesweeper {
    [DesignerCategory("")]
    internal class DoubleBufferedPanel : Panel {
        public DoubleBufferedPanel() {
            DoubleBuffered = true;
        }
    }
}
