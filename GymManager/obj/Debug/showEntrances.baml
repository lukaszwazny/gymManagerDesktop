   M S B A M L   `   `   `  ����E  AGymManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GymManager
GymManager  5          GymManager.showEntrances   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:GymManager     5        N� Title$  showEntrances��5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��entrancesList��5         AutoGenerateColumns .  $
��Left=�6D   $	��644��6_   $	��Top=�6l   $
��1108��6�      
IsReadOnly . $��0,25,0,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Imię��5   !   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *Name+   HeaderTemplate 5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6)   .+�5   "   �� ��6,   �� *	Content+ �� RelativeSource �� 	 �� Mode$	 TemplatedParent=��� 5   "   
 ��Name$
 txtName��6*   -   5        $	��100��5   ;   6G   5      5      5        5      $ Nazwisko��5   !    5   %   �� *	Surname+ 5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 5!   "   $
 
txtSurname��6*   -   5        $	��100��5!   >   6J   5"      5#      5%        5&      $ Nazwa karnetu��5'   !    5&   %   �� *packageName+ 5(      �� 5)      .R�5*      �� $ 
Horizontal=�6)   .+�5+   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 5-   "   $
 txtpackageName��6*   -   5        $	��100��5-   B   6N   5.      5/      51        52      $ Data wejścia��53   !    52   %   �� *entranceDate+ �� StringFormat$ {0:dd.MM.yyyy}�� 54      �� 55      .R�56      �� $ 
Horizontal=�6)   .+�57   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 59   "   $
 txtentranceDate��6*   -   5        $	��100��59   C   6O   5:      5;      5=      5?      �� 5@   
   $
��Left=�6   $��10,0,0,0q�6/   $
��Wrap=�6A   $��Szukaj: ��6U   $	��Top=�6e   $��20��6}   �� 5A   
   -   5        $
��Left=�5A      $��20��6-   $��54,0,0,0q�69    �� TextWrapping$
 Wrap=�6K   $�� ��6_   $	��Top=�6g   $	��120��6   6�   �� 5B   
   -   5        $��whatToSearch��5B      $
��Left=�6'   $��	179,0,0,0q�6B   $	��Top=�6U   $	��120��6m   .@�5C      �� $��Imię��6   $
��Left=�6*   $	��118��6E   �� 5D      $��Nazwisko��6   $
��Left=�6.   $	��118��6I    �� 
IsSelected . 6U   �� 5E      $��Nazwa karnetu��6   $
��Left=�63   $	��118��6N   �� 5F      $��Data wejścia��6   $
��Left=�62   $	��118��6M   5G      5H      5J      