[Root]/Controllers/ManagerController.cs => Index function shows the differences between eager loading and page loading

1. For the viewModel.Managers loading, I have used eager loading.
It is because
- The Managers view alwasy requires the OfficeAssignment entity so it's more efficient to fetch that in the same query.

2. For the viewModel.Projects loading, I have used lazy loading.
It is because

Seeing the view, when the user select the manager, related Project entities are displayed.
The Manager and Project entities are in a many-to-many relationship.
In this case, lazy loading is more efficient because the user need projects only for the selected manager.
